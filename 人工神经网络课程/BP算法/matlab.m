function bp3 
 
clear all; 
clc 
 
"n=input('输入层神经元个数:'); "
中间层神经元个数=input('中间层神经元个数:'); 
"1=input('输出层神经元个数:'); "
学习率alpha=input('输入学习率alpha:'); 
学习率beta=input('输入学习率beta:'); 
mode=input('选择单极或双极函数，单极输入1，双极输入2:'); 
总循环数=input('最大训练次数选择:'); 
 
X1=[1 1 1 1 1 0 0 1 1 1 1 1 1 0 0 1]; 
X2=[0 1 0 0 0 1 0 0 0 1 0 0 0 1 0 0]; 
X3=[1 1 1 1 1 0 0 1 1 0 0 1 1 1 1 1]; 
X=[X1;X2;X3]; 
if(mode==1) 
    Y1=[1 0 0]; 
    Y2=[0 1 0]; 
    Y3=[0 0 1]; 
else 
    Y1=[1 -1 -1]; 
    Y2=[-1 1 -1]; 
    Y3=[-1 -1 1]; 
end 
Yo=[Y1;Y2;Y3]; 
 
中间层=zeros(1,中间层神经元个数); 
ct=zeros(1,1); 
dt=zeros(1,1); 
最大误差=0.001; 
当前循环数=0; 
输入层到中间层的权矩阵=rands(2,中间层神经元个数); 
S='初始化连接权为:'; 
disp(S); 
disp(输入层到中间层的权矩阵); 
中间层到输出层的权矩阵=rands(中间层神经元个数,1); 
t1=rand(1,中间层神经元个数); 
t2=rand(1,1); 
当前误差=0.012; 
输出=zeros(3,3); 
 
for 当前循环数=1:总循环数 
    if (当前误差>最大误差) 
        当前误差=0; 
        for cp=1:3 
            X0=X(cp,:); 
            Y=X0*输入层到中间层的权矩阵; 
            Y0=Yo(cp,:); 
            Y=Y-t1; 
            for j=1:中间层神经元个数 
                if(mode==1) 
                    中间层(j)=1/(1+exp(-Y(j))); 
                else 
                    中间层(j)=2/(1+exp(-Y(j)))-1; 
                end 
            end 
            xy=中间层*中间层到输出层的权矩阵; 
            Y=xy; 
            Y=Y-t2; 
            for t=1:1 
                if(mode==1) 
                    ct(t)=1/(1+exp(-Y(t))); 
                    else 
                    ct(t)=2/(1+exp(-Y(t)))-1; 
                end 
            end 
            当前误差=当前误差+sum((Y0-ct).*(Y0-ct))/2; 
            for t=1:1 
                if(mode==1) 
                dt(t)=(Y0(t)-ct(t))*ct(t)*(1-ct(t)); 
                else 
                dt(t)=(Y0(t)-ct(t))*(1-ct(t)*ct(t))/2; 
                end 
            end 
            xy=dt*中间层到输出层的权矩阵的转置; 
            if(mode==1) 
                ei=xy.*中间层.*(1-中间层); 
            else 
                ei=xy.*(1-中间层.*中间层)/2; 
            end 
            for t=1:1 
                for j=1:中间层神经元个数 
                    中间层到输出层的权矩阵(j,t)=中间层到输出层的权矩阵(j,t)+学习率alpha*dt(t)*中间层(j); 
                end 
                t2(t)=t2(t)+学习率alpha*dt(t); 
            end 
            for j=1:中间层神经元个数 
                for k=1:2 
                    输入层到中间层的权矩阵(k,j)=输入层到中间层的权矩阵(k,j)+学习率beta*X0(k)*ei(j); 
                end 
                t1(j)=t1(j)+学习率beta*ei(j); 
            end 
            输出(cp,:)=ct; 
        end 
	else break; 
    end 
end 
 
输出=输出'; 
S='训练次数cnt:'; 
disp(S); 
disp(当前循环数); 
S='输出向量:'; 
disp(S); 
disp(输出); 
S='全局误差er:'; 
disp(S); 
disp(当前误差);

