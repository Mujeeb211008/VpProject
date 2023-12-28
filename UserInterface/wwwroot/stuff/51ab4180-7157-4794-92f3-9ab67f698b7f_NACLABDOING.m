%Given data
x=input('enter the data of absciassas:')
y=input('enter the data of ordinates:')
P0=input('enter the point at which you want the approximation')
rows=length(x)
cols=rows+1
h=x(2)-x(1)
F=zeros(rows,cols)
%add data to the table
for i=1:rows
    F(i,1)=x(i)
    F(i,2)=y(i)
end
%find deltas
n=1 %helping variable
for j=3:cols
    for i=1:rows-n
        F(i,j)=F(i+1,j-1)-F(i,j-1)
    end
    n=n+1
end
%find y0
u=0
for i=1:rows
    T=(P0-x(i))/h
    if((T>0&&T<1))
        u=T
        r=i
        k=x(r)
    end
end
u
F0=F(r,2)
dF1=F((r-1),3)
dF0=F(r,3)
d2F1=F((r-1),4)
d3F1=F((r-2),5)
d3F2=F((r-1),5)
d4F1=F((r-2),6)
Fp=F0+(u*(dF0+dF1))/2+((u*u)*d2F1)/2+(u*((u*u)-1)*(d3F1+d3F2))/12+((u*u)*(u*u)-1)*d4F1/24