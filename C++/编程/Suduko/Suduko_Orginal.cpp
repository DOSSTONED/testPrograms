#include <iostream>
using namespace std;




int main()
{
int suduko[9][9][10];
int i,j,k,aa,z,max;
int bb,cc,dd,ee;
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
for(max=0;max<32767;max++)
{
/*�����������,��*/
for(i=0;i<9;i++)
{
for(j=0;j<9;j++)
{
  if(suduko[i][j][0]!=0)
   {
    k=suduko[i][j][0];
    for(z=0;z<9;z++)
    suduko[i][z][k]=0;
   }
  else
   {
    aa=0;
    for(k=1;k<10;k++)
    aa=aa+suduko[i][j][k];
    if(aa==1)
     {
      for(k=1;k<10;k++)
      {
      if(suduko[i][j][k]==1)
       suduko[i][j][0]=k;
      }
     }
   }
}
}
/*�����������,��*/
for(i=0;i<9;i++)
{
for(j=0;j<9;j++)
{
  if(suduko[i][j][0]!=0)
   {
    k=suduko[i][j][0];
    for(z=0;z<9;z++)
    suduko[z][j][k]=0;
   }
  else
   {
    aa=0;
    for(k=1;k<10;k++)
    aa=aa+suduko[i][j][k];
    if(aa==1)
     {
      for(k=1;k<10;k++)
      {
      if(suduko[i][j][k]==1)
       suduko[i][j][0]=k;
      }
     }
   }
}
}
/*�����������,��*/
for(bb=0;bb<9;bb=bb+3)
{
for(cc=0;cc<9;cc=cc+3)
{
  for(i=0;i<3;i++)
  {
   for(j=0;j<3;j++)
   {
    if(suduko[i+bb][j+cc][0]==0)
     {
      aa=0;
      for(k=1;k<10;k++)
      aa=aa+suduko[i+bb][j][k];
      if(aa==1)
       {
        for(k=1;k<10;k++)
        {
         if(suduko[i+bb][j][k]==1)
          suduko[i+bb][j][0]=k;
        }
       }
     }
    else
     {
      k=suduko[i+bb][j+cc][0];
      for(dd=0;dd<3;dd++)
      {
       for(ee=0;ee<3;ee++)
       suduko[bb+dd][cc+ee][k]=0;
      }
     }
   }
  }
}
}
/*�������*/
}
for(i=0;i<9;i++)
{
  for(j=0;j<9;j++)
   printf("%5d",suduko[i][j][0]);
  printf("\n");
}
printf("o!");
scanf("%d",dd);

return 0;
}
/*�������*/
