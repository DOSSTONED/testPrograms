#include "stdio.h"
#include "stdlib.h"
#include "math.h"
#include "time.h"

void main()
{
	int n,N,r;   //n为每幅人脸图像的像素点数，N为人脸图像数目,r为分解后的维数
	n = 4;
	N = 3;
	r = 2;
	double *B = new double[n*r];             //分解后的基向量
	double *H = new double[r*N];             //分解后的系数矩阵
	double *OriginalImage = new double[n*N]; //所有人脸图像组成的矩阵
	double *NewImage = new double[n*N];      //每次更新过程中保存BH的值

	int i, j, k;
	/////////////////给待分解矩阵赋值////////////////////////
	int temp = 1;
	for(i = 0; i < n; i++)
	{
		for(j = 0; j < N; j++)
		{
			OriginalImage[i*N+j] =  temp;
			temp++;
		}
	}
	/////////////////初始非0矩阵 B，H/////////////////////////
	for(i = 0; i < n; i++)
		for(j = 0; j < r; j++)
			B[i*r+j] = 0.5;
	for(j = 0; j < r; j++)
		for(k = 0; k < N; k++)
			H[j*N+k] = 0.8;

	//////更新B，H，直到收敛
	int ii,jj,kk;
	double temp1,temp2;
	double distance;
	int count = 0;
	do
	{
		count++;
		for(j = 0; j < r; j++)   
		{  //计算BH
			for(ii = 0; ii < n; ii++)
			{
				for(kk = 0; kk < N; kk++)
				{
					temp1 = 0.0;
					for(jj = 0; jj < r; jj++)
						temp1+=B[ii*r+jj]*H[jj*N+kk];
						NewImage[ii*N+kk] = temp1;
				}
			}	
			for( k = 0; k < N; k++) //更新H一行
			{
				temp1 = 0.0;
				temp2 = 0.0;
				for(ii = 0; ii < n; ii++)
				   temp1 += B[ii*r+j]*OriginalImage[ii*N+k]/NewImage[ii*N+k];
				for(ii = 0; ii < n; ii++)
					temp2 += B[ii*r+j];
				H[j*N+k] = H[j*N+k]*temp1/temp2;		
			}
			//计算BH
			for(ii = 0; ii < n; ii++)
			{
				for(kk = 0; kk < N; kk++)
				{
					temp1 = 0.0;
					for(jj = 0; jj < r; jj++)
						temp1+=B[ii*r+jj]*H[jj*N+kk];
						NewImage[ii*N+kk] = temp1;
				}
			}
			for(i = 0; i < n; i++)  //更新B一列
			{
				temp1 = 0.0;
				temp2 = 0.0;
				for(k = 0; k < N; k++)
					temp1 += (H[j*N+k]*OriginalImage[i*N+k]/NewImage[i*N+k]);
				for(ii = 0; ii < N; ii++)
					temp2 += H[j*N+ii];
				B[i*r+j] = B[i*r+j]*temp1/temp2;
			}		
		}
		//重新计算BH
		for(ii = 0; ii < n; ii++)
		{
			for(kk = 0; kk < N; kk++)
			{
				temp1 = 0.0;
				for(jj = 0; jj < r; jj++)
					temp1+=B[ii*r+jj]*H[jj*N+kk];
					NewImage[ii*N+kk] = temp1;
			}
		}
		//计算OriginalImage和BH之间的前差异距离
		distance = 0;
		for(ii = 0; ii < n; ii++)
		{
			for(jj = 0; jj < N; jj++)
			{
				distance += (OriginalImage[ii*N+jj]*log(OriginalImage[ii*N+jj]/NewImage[ii*N+jj]) - OriginalImage[ii*N+jj] + NewImage[ii*N+jj]);
			}			
		}
	
	}while((fabs(distance)>0.0001)&&(count < 100));
	
	double *DiffX = new double[n*N];
	for(i = 0; i < n; i++)
		for(j = 0; j < N; j++)
			DiffX[i*N+j] = OriginalImage[i*N+j] - NewImage[i*N+j];
	printf("Count:%d\ndistance:%lf\n",count ,distance);
	printf("Original Matrix:\n");
	for(i=0;i<n;i++)
	{
		for(j=0;j<N;j++)
			printf("%10.5f",OriginalImage[i*N+j]);
		printf("\n");
	}
    printf("B:\n");
	for(i=0;i<n;i++)
	{
		for(j=0;j<r;j++)
			printf("%10.5f",B[i*r+j]);
		printf("\n");
	}
	printf("H:\n");
	for(i=0;i<r;i++)
	{
		for(j=0;j<N;j++)
			printf("%10.5f",H[i*N+j]);
		printf("\n");
	}
	printf("DiffOriginalImage:\n");
	for(i = 0; i < n; i++)
	{
		for(j = 0; j < N; j++)
			printf("%10.5f",DiffX[i*N+j]);
		printf("\n");
	}
}