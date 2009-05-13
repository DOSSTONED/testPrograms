#include <iostream>
#include <math.h>
using namespace std;

int f(int a)
{
	int b,c,d;
	b=(a%10)*pow(10,int(log10(double(a))))+(a-a%10)/10;
	c=b*b;
	d=c%int(pow(10,int(log10(double(c)))))*10+(c-c%int(pow(10,int(log10(double(c))))))/int(pow(10,int(log10(double(c)))));
	return d;
}

int main()
{
	int n,i,count=1,a;
	cin>>n;
	for(i=1;count<=n;i++)
	{
		if(f(i)==i*i)
		{
			count++;
			a=i;
		}
	}
	cout<<a<<endl;
	return 0;
}
