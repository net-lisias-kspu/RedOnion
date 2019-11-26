﻿## WIP Dev notes:

   https://stackoverflow.com/questions/26347022/meaning-of-the-from-parameter-of-lua-resume

Seems that the NLua.Lua instance is a thread and if you mess it up you cannot resume.
I think you can resume from an error with pcallk but not sure about infinite loop. Perhaps.
But I've chosen to solve this problem another way.

Don't know of a way to pop objects safely off a non main thread except by using otherThread.xmove(mainThread,n) to move
n objects from the non-main thread's stack to the main thread's stack and then using mainThread.Pop() to pop them off.

Popping a function off the stack with ToCFunction+Pop(1) and then putting it on again doesn't work.
Looking at their implementation of NLua.Lua.Pop, Popping Userdata off the stack with
KeraLua.Lua.Pop(n) after trying to use `runningThread.ToUserData` seems not safe.

Execution system for our requirements, that I've deciphered:
You have main thread in your `Lua state=new Lua();`
Then you create a thread: `KeraLua.Lua thread=state.State.NewThread();`
This thread will share an environment, including globals, with the main thread `state`, which is represented by the Lua class.
Now to set some `source` code for execution you say `thread.LoadString(source)`.
That compiles the code in `source` and places the resulting executable code in a function on top of the stack.
Then you call `thread.Resume(null,0)` to start that executable code. `thread.Resume(null,0)` calls the function on top of the stack,
which is the function we just placed on it. Check lua docs for lua_resume to find out more. 
We could have also used the function `thread.Call` to call the code, but using `thread.Resume` allows the code to be 
interrupted and then resumed.

I believe that when you call `thread.Resume` with a function on the stack, that thread becomes dedicated to executing that code
until it is finished. So I'm creating new `threads` each time we need to start a new execution (in case the code errors out, 
or never ends).

Threads started with `thread.Resume` can be interrupted either with yields called inside the code,
or by setting up a `hook` function.

`state.State.SetHook(AutoyieldHook, KeraLua.LuaHookMask.Count, 1000);`

```C#
private void AutoyieldHook(IntPtr luaState, IntPtr ar)
{
    var keraluaState = KeraLua.Lua.FromIntPtr(luaState);
    
    if (keraluaState.IsYieldable)
    {
        keraluaState.Yield(0);
    }
}
```

The hook function, `AutoyieldHook` had been set up to be automatically called every 1000 instructions.
Normally this hook functionality is used for debugging, and can also be set to execute every line, every function call, or every return.
Users can set the hook in lua code (Example: `debug.sethook(hookfunction,'c',1000)`).
As far as I know, only one hook can be active at once, so debugging features will have to be done thoughtfully.


`thread.Resume` also traps errors, and will return error information as if it had yielded.


Interesting things:
- `panic function`
- `error handling`
- `you can change the metatable of other types by using the debug library`
- `userdata can have metatables`
- `string substitution`
- `string.dump(function)` binary representation of a function
- `string.pack` for serialization
- `table.sort`
- `__gc` metamethod for finalizing objects prior to garbage collection
- `can control gc` can stop and start it and define out aggressive it is.
- `there are lots of reflection helpers in nlua`
- `lua.GetTable(path) for searching through many nested tables`
- `The actual tables are all implemented in and accessed through C`
- `Refs`

In a test
```
starttime=time()
sethookcount(100000000)
sum=0
for i=1,10000000 do sum=sum+i end
print("sum is "..sum)
endtime=time()
print(endtime-starttime)
```

Nlua completed this in a few seconds, MLua took about 20. Nlua maybe is 10 times faster for pure lua code.

But on a test for a call to a dotnet function, they took about the same time.

A test where an argument is passed, slowed NLua down way more. MLua beat it by a factor of 3.
```
starttime=time()
sum=0
for i=1,1000000 do sethookcount(i) end
print("sum is "..sum)
endtime=time()
print(endtime-starttime)
sethookcount(100000000)
```