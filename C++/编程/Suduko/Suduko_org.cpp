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
    scanf("%d",&suduko[j][0]);
    if (suduko[j][0]==0)
     for(k=1;k<10;k++)
     suduko[j][k]=1;
    else
     for(k=1;k<10;k++)
     suduko[j][k]=0;
  }
}
for(max=0;max<32767;max++)
{
/*下面进行填数,列*/
for(i=0;i<9;i++)
{
for(j=0;j<9;j++)
{
  if(suduko[j][0]!=0)
   {
    k=suduko[j][0];
    for(z=0;z<9;z++)
    suduko[z][k]=0;
   }
  else
   {
    aa=0;
    for(k=1;k<10;k++)
    aa=aa+suduko[j][k];
    if(aa==1)
     {
      for(k=1;k<10;k++)
      {
      if(suduko[j][k]==1)
       suduko[j][0]=k;
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
  if(suduko[j][0]!=0)
   {
    k=suduko[j][0];
    for(z=0;z<9;z++)
    suduko[z][j][k]=0;
   }
  else
   {
    aa=0;
    for(k=1;k<10;k++)
    aa=aa+suduko[j][k];
    if(aa==1)
     {
      for(k=1;k<10;k++)
      {
      if(suduko[j][k]==1)
       suduko[j][0]=k;
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
    if(suduko[j+cc][0]==0)
     {
      aa=0;
      for(k=1;k<10;k++)
      aa=aa+suduko[j][k];
      if(aa==1)
       {
        for(k=1;k<10;k++)
        {
         if(suduko[j][k]==1)
          suduko[j][0]=k;
        }
       }
     }
    else
     {
      k=suduko[j+cc][0];
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
/*填数完毕*/
}
for(i=0;i<9;i++)
{
  for(j=0;j<9;j++)
   printf("%5d",suduko[j][0]);
  printf("\n");
}
printf("o!");
scanf("%d",dd);

return 0;
}
/*程序结束*/
