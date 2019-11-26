starttime=time.now
setexeclimit(10000000000)
sum=0
for i=1,10000000 do sum={1} end
print("sum is ")
endtime=time.now
print(endtime-starttime) -- 34 seconds