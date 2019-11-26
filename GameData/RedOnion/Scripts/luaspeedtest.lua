starttime=time.now

sum=0
for i=1,1000000 do setexeclimit(i) end
print("sum is ")
endtime=time.now
print(endtime-starttime) -- 34 seconds
setexeclimit(10000000000)