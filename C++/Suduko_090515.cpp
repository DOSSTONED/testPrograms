#include <iostream>
#include <fstream>
using namespace std;

int fillsudu(int fill[][9][10])
{
	/*下面进行填数,列*/
	int i=0,j=0,k=0,aa=0,z=0,max=0;
	int bb=0,cc=0,dd=0,ee=0,status=0;//status==1 说明数独没填满
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			if(fill[i][j][0]!=0)
			{
				
				k=fill[i][j][0];
				//cout<<k;
				for(z=0;z<9;z++)
				{
					fill[z][j][k]=0;
					fill[i][z][k]=0;
					fill[i][j][z+1]=0;
				}
			}
			else
			{
				status=1;
				aa=0;
				for(k=1;k<10;k++)
				aa=aa+fill[i][j][k];
				if(aa==1)
				{
					for(k=1;k<10;k++)
					{
						if((fill[i][j][k]==1)&&(fill[i][j][0]==0))
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
				status=1;
				aa=0;
				for(k=1;k<10;k++)
				aa=aa+fill[i][j][k];
				if(aa==1)
				{
					for(k=1;k<10;k++)
					{
						if((fill[i][j][k]==1)&&(fill[i][j][0]==0))
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
						status=1;
						aa=0;
						for(k=1;k<10;k++)
						aa=aa+fill[i+bb][j][k];
						if(aa==1)
						{
							for(k=1;k<10;k++)
							{
								if( (fill[i+bb][j][k]==1)&&(fill[i+bb][j][0]==0) )
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
	/*某一个数在某一个9格中仅只能出现在一个位置时的填数*/
	{
		for(k=1;k<10;k++)
		{
			for(bb=0;bb<9;bb=bb+3)
			{
				for(cc=0;cc<9;cc=cc+3)
				{
					aa=0;
					for(i=0;i<3;i++)
					{
						for(j=0;j<3;j++)
						{
							aa=aa+fill[bb+i][cc+j][k];
							if(fill[bb+i][cc+j][k]==0)
							status=1;
						}
					}
					if(aa==1)
					{
						for(i=0;i<3;i++)
						{
							for(j=0;j<3;j++)
							{
								if((fill[bb+i][cc+j][k]==1)&&(fill[bb+i][cc+j][0]==0))
								{
									fill[bb+i][cc+j][0]=k;
								}
							}
						}
					}
				}
			}
		}

	}

	/*某一个数在某一行中仅只能出现在一个位置时的填数*/
	/*
	{
		for(k=1;k<10;k++)
		{
			
			for(i=0;i<9;i++)
			{
				aa=0;
				for(j=0;j<9;j++)
				{
					aa=aa+fill[i][j][k];
				}
				if(aa==1)
				{
					for(j=0;j<9;j++)
					{
						if(fill[i][j][k]==1&&(fill[i][j][0]==0))
						{
							fill[i][j][0]=k;
						}
					}
				}
			}
		}

	}
	*///去掉这些行之后没有screenshot.10.png的错误了
	/*填数完毕*/
	
	return status;
}

/*
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
*/

int trysuduko(int fill[][9][10])
{
	int i,j,z;
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			if(fill[i][j][0]==0)
			{
				for(z=1;z<10;z++)
				{
					if(fill[i][j][z]==0)
					{
						fill[i][j][0]=z;
						fill[i][j][z]=-1;//标记为-1的表示正在尝试
						break;
					}
				}
			}
		}
	}
	return 0;
}

const int BIAN=3000;//穷举遍数 

int main()
{
	int suduko[9][9][10];
	int sudukobackup[9][9];
	int i,j,k,aa=0,z=0,max=0;
	int sum=0,filledstatus=1;
	//int bb,cc,dd,ee;

	// File input
	{
		fstream in("suduko.txt");
		for(i=0;i<9;i++)
		{
			for(j=0;j<9;j++)
			{
				in>>suduko[i][j][0];
				
				if (suduko[i][j][0]==0)
				for(k=1;k<10;k++)
				suduko[i][j][k]=1;
				else
				for(k=1;k<10;k++)
				suduko[i][j][k]=0;
			}
		}
		in.close ();
	}

	//
	/*
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
	*/


	//计算 
	for(max=0;max<BIAN;max++)
	{
		aa=fillsudu(suduko);
			if(aa==0)break;
	}
	
	cout<<"max:"<<max<<endl;
	/*
	int unfilled[80]={0},u1;
	for(i=0;i<9;i++)
	{
	for(j=0;j<9;j++)
	{
	if(suduko[i][j][0]==0)
	{
	unfilled[u1]=10*i+j;
	filledstatus=0;
	}
	
	}
	}
	
	*/
	
	/*
	if(filledstatus==0)
	{
	copysuduko(suduko,sudukobackup);
	}
	*/
	//
	
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		printf("%5d",suduko[i][j][0]);
		printf("\n");
	}
	if(filledstatus==0)
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

