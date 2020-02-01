/*
//#include<iostream>
//#include<string>
#include<cstdio>
//using namespace std;
int main(void)
{
	char *a;
	a=new char[100];
	scanf("%s",a);
	//cout<<"Using cout function:\t"<<a<<endl;
	printf("Using printf function:\t%s\n",a);
	printf("Print char by char:\n");
	for(int i=0;i<100;i++)
		printf("%x\n",a[i]);
	return 0;
}

#include<iostream>
#include<string>
using namespace std;
int main(void)
{
	/////////////////////////////////////////////
	
	char a[5][10];
	for(int i=4;i>=0;i=i-1)
		for(int j=9;j>=0;j=j-1)
			cin>>a[i][j];
		for(i=0;i<5;i=i+1)
		{
			for(int j=0;j<10;j=j+1)
				cout<<a[i][j];
			//cout<<endl;
		}
		/////////////////////////////////////////////
		string String1,String2,StringSum;
		int String1_Size,String2_Size,i,Carry;
		cin>>String1>>String2;
		String1_Size=String1.size();
		String2_Size=String2.size();
		//for(i=0;i<String2_Size;i++)
		//printf("%x\n",String2[i]);
		if(String1_Size>String2_Size)
			StringSum=String1;
		else
			StringSum=String2;
		//StringSum_Size=StringSum.size();
		for(i=StringSum.size();i>=0;i--)
			if()
				StringSum[i]=String1[StringSum.size()-String1_Size]+String2[StringSum.size()-String2_Size];
			cout<<String1_Size<<"\nString1:\t"<<String1<<endl<<String2_Size<<"\nString2:\t"<<String2<<endl<<"StringSum:\t"<<StringSum<<endl;
			//for(i=0;i<StringSum.size();i++)
			//printf("%x\n",StringSum[i]);
			return 0;
}*/
#include<iostream>
#include<string>
using namespace std;
int CheckString(string String1,string String2)
{
	int i;
	for(i=0;i<String1.size();i++)
	{
		if(String1[i]<0x30||String1[i]>0x39)
			return 0;
	}
	for(i=0;i<String2.size();i++)
	{
		if(String2[i]<0x30||String2[i]>0x39)
			return 0;
	}
	return 1;
}
int main(void)
{
	string String1,String2,Max,Min;
	int i=0,Status=1,Carry=0;
	int StringSum[100]={0};
	
	while(Status==1)
	{
		cout<<"请输入加数";
		cin>>String1;
		cout<<"请输入被加数";
		cin>>String2;
		
		if(CheckString(String1,String2)==1)
		{
			if(String1.size()>String2.size())
			{
				Max=String1;
				Min=String2;	
			}
			else
			{
				Max=String2;
				Min=String1;
			}
			for(i=Max.size()-1;i>=0;i--)
			{
				if((i+Min.size())>=(Max.size()))
				{
					if(Max[i]+Min[i-Max.size()+Min.size()]+Carry>105)
					{
						StringSum[i+1]=Max[i]+Min[i-Max.size()+Min.size()]+Carry-0x60-10;
						Carry=1;
					}
					else
					{
						StringSum[i+1]=Max[i]+Min[i-Max.size()+Min.size()]+Carry-0x60;
						Carry=0;
					}
				}
				else
				{
					if(Max[i]+Carry>57)
					{
						StringSum[i+1]=Max[i]+Carry-0x30-10;
						Carry=1;
					}
					else
					{
						StringSum[i+1]=Max[i]+Carry-0x30;
						Carry=0;
					}
				}
			}
			StringSum[0]=Carry;
			if(StringSum[0]!=0)
				cout<<StringSum[0];
			for(i=1;i<=Max.size();i++)
				cout<<StringSum[i];
			Status=0;
			return 0;
		}
		cout<<"输入有误，请重新输入\n";
	}
	return 0;
}