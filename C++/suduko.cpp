#include <iostream>
using namespace std;

int fill(int fill[][9][10])
{
	/*下面进行填数,列*/
	int i=0,j=0,k=0,aa=0,z=0,max=0;
	int bb=0,cc=0,dd=0,ee=0;
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			if(fill[i][j][0]!=0)
			{
				k=fill[i][j][0];
				for(z=0;z<9;z++)
					fill[i][z][k]=0;
			}
			else
			{
				aa=0;
				for(k=1;k<10;k++)
					aa=aa+fill[i][j][k];
				if(aa==1)
				{
					for(k=1;k<10;k++)
					{
						if(fill[i][j][k]==1)
							fill[i][j][0]=k;
					}
				}
			}
		}
	}
	/*下面进行填数,行*/
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			if(fill[i][j][0]!=0)
			{
				k=fill[i][j][0];
				for(z=0;z<9;z++)
					fill[z][j][k]=0;
			}
			else
			{
				aa=0;
				for(k=1;k<10;k++)
					aa=aa+fill[i][j][k];
				if(aa==1)
				{
					for(k=1;k<10;k++)
					{
						if(fill[i][j][k]==1)
							fill[i][j][0]=k;
					}
				}
			}
		}
	}
	/*下面进行填数,格*/
	for(bb=0;bb<9;bb=bb+3)
	{
		for(cc=0;cc<9;cc=cc+3)
		{
			for(i=0;i<3;i++)
			{
				for(j=0;j<3;j++)
				{
					if(fill[i+bb][j+cc][0]==0)
					{
						aa=0;
						for(k=1;k<10;k++)
							aa=aa+fill[i+bb][j][k];
						if(aa==1)
						{
							for(k=1;k<10;k++)
							{
								if(fill[i+bb][j][k]==1)
									fill[i+bb][j][0]=k;
							}
						}
					}
					else
					{
						k=fill[i+bb][j+cc][0];
						for(dd=0;dd<3;dd++)
						{
							for(ee=0;ee<3;ee++)
								fill[bb+dd][cc+ee][k]=0;
						}
					}
				}
			}
		}
	}
	/*每个状态在试验的时候标记为-1，不能填标记为0，可以填标记为1。*/
	/*填数完毕*/
    
	return 1;
}

int copysuduko(int insuduko[9][9][10],int outsuduko[9][9])
{
	for(int i=0;i<9;i++)
	{
		for(int j=0;j<9;j++)
		{
			outsuduko[i][j]=insuduko[i][j][0];
		}
	}
	return 0;
}


int backsuduko(int outsuduko[9][9][10],int insuduko[9][9])
{
	for(int i=0;i<9;i++)
	{
		for(int j=0;j<9;j++)
		{
			outsuduko[i][j][0]=insuduko[i][j];
		}
	}
	return 0;
}




const int BIAN=30000;//穷举遍数 

int main()
{
	int suduko[9][9][10];
	int sudukobackup[9][9];
	int i,j,k,aa=0,z=0,max=0;
	int sum=0,filledstatus=0;
	//int bb,cc,dd,ee;
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			scanf("%d",&suduko[i][j][0]);
			
			if (suduko[i][j][0]==0)
				for(k=1;k<10;k++)
					suduko[i][j][k]=1;
				else
					for(k=1;k<10;k++)
						suduko[i][j][k]=0;
		}
	}
	//计算 
	for(max=0;max<BIAN;max++)
	{
		filledstatus=fill(suduko);
		if(filledstatus==1)
			break;
	}
	
	//
	
	//
	
	if(filledstatus==0)
	{
		copysuduko(suduko,sudukobackup);
	}
	
	//
	
	/*
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
			printf("%5d",suduko[i][j][0]);
		printf("\n");
	}
	*/
	if(filledstatus==1)
	{
		for(i=0;i<9;i++)
		{
			for(j=0;j<9;j++)
				printf("%5d",sudukobackup[i][j]);
			printf("\n");
		}
	}
	printf("o!");
	system("pause");
	
	return 0;
}
/*程序结束*/

