#include <iostream>
#include <fstream>
using namespace std;

int fillsudu(int fill[][9][10])
{
	/*下面进行填数,列*/
	int i=0,j=0,k=0,aa=0,z=0,max=0;
	int bb=0,cc=0,dd=0,ee=0,status=0,changed=0;//status==1 说明数独没填满,changed非0为已改动
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
						{
							changed++;
							fill[i][j][0]=k;
						}
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
						{changed++;
						fill[i][j][0]=k;}
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
								{changed++;
								fill[i+bb][j][0]=k;
								}
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
									changed++;
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
	
	return changed;//status useless
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

int trysuduko(int fill[][9][10],int tryrecord[][9],int signal)//signal 指是否填错了，-1为没有，1表示填错了，也就是最后一个尝试是错误的
{
	int i,j,z;
	if(signal==-1)
	{
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			if(fill[i][j][0]==0)
			{
				for(z=1;z<10;z++)
				{
					if(fill[i][j][z]==1)
					{
						fill[i][j][0]=z;
						fill[i][j][z]=0;
						tryrecord[i][j]=z;//标记为非0的表示正在尝试，z表示正在尝试的数
						return 0;
					}
				}
				cout<<"这个地方不能填任何数！"<<endl;
				return 0;
			}
		}
	}
	}
	if(signal==1)
	{
		for(i=8;i<0;i--)
		{
			for(j=8;j<0;j--)
			{
				if(tryrecord[i][j]!=0)
				{
					z=tryrecord[i][j];
					fill[i][j][0]=0;
					fill[i][j][z]=0;
					tryrecord[i][j]=0;
					return 0;
				}
			}
		}
	}
	return 0;
}

int checksuduko(int check[][9][10])// 0 for normal ; 1 for wrong suduko; -1 for not filled up yet;
{
	int i=0,j=0,k=0,bb=0,cc=0,status=0;
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			if(check[i][j][0]!=0)
			{
				for(k=0;k<9;k++)
				{
					if( (k!=i) && (check[i][j][0] == check [k][j][0]) )
						return 1;
					if( (k!=j) && (check[i][j][0] == check [i][k][0]) )
						return 1;
				}
				for(bb=0;bb<3;bb++)
				{
					for(cc=0;cc<3;cc++)
					{
						if( (check[i/3*3+bb][j/3*3+cc][0] == check[i][j][0]) && ((i/3*3+bb)!=i) && ((j/3*3+cc)!=j) )
							return 1;
					}
				}
			}
			else
			{
				return -1;
			}
		}
	}
	return 0;
}

int clearsuduko(int clear[][9][10])
{
	int i=0,j=0,k=0,z=0,bb=0,cc=0;
	for(i=0;i<9;i++)
	{
		for(j=0;j<9;j++)
		{
			if(clear[i][j][0]!=0)
			{
				k=clear[i][j][0];
				//cout<<k;
				for(z=0;z<9;z++)
				{
					clear[z][j][k]=0;
					clear[i][z][k]=0;
					//clear[i][j][z+1]=0;
				}
				for(bb=0;bb<3;bb++)
				{
					for(cc=0;cc<3;cc++)
					{
						clear[i/3*3+bb][j/3*3+cc][k] =0;
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
	//int sudukobackup[9][9];
	int i,j,k,aa=0,z=0,max=0;
	int sum=0,filledstatus=1;
	//int bb,cc,dd,ee;

	// File input
	{
		fstream in("suduko_hard.txt");
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

	int tryrecord[9][9]={0};

	for(max=0;max<BIAN;max++)
	{
		clearsuduko(suduko);
		aa=checksuduko(suduko);
		trysuduko(suduko,tryrecord,aa);
		if(checksuduko(suduko)==1)
			continue;
		for(i=0;i<BIAN;i++)
		{
			aa=fillsudu(suduko);
				if(aa==0)break;
		}
			if(checksuduko(suduko)==0)break;
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
	

	if(checksuduko(suduko)!=2)
	{
		if(checksuduko(suduko)==-1)
	{
		printf("%s","数独未完成！\n");
	}
		for(i=0;i<9;i++)
		{
			for(j=0;j<9;j++)
			printf("%5d",suduko[i][j][0]);
			printf("\n");
		}
	}
	

	if(checksuduko(suduko)==1)
	{
		printf("%s","数独有错！");
	}
	printf("o!\n");

	system("pause");
	
	return 0;
}
/*程序结束*/

