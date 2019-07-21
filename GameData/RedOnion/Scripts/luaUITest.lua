UI=import.RedOnion.UI
w=new(UI.Window,"Window",UI.Layout.None)

p=new(UI.Panel)
w.Add(p)
p.Layout=UI.Layout.Vertical
p.ChildAnchors=UI.Anchors.Fill
b=new(UI.Button)
b.text="buttontext"
p.Add(b)

b.click.Add(function() for i=1,100000 do print(i) end end)
