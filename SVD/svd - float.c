/*
RESULT:
__WINDOWS:
Function svd() consumes 0.218000s.
The difference between a and a_bak is: 18024359.320355.

__LINUX_FEDORA_X86_64

*/
#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#include "math.h"
#include "time.h"

//#include "conio.h"
#define m 6
#define n 6
#define RepeatedTimes 10000

FILE *file1 = NULL;
FILE *file2 = NULL;
float a[RepeatedTimes][m][n], a_bak[RepeatedTimes][m][n];
double diffBetween_a_a_bak = 0;
float w[RepeatedTimes][n],t[n];
float v[RepeatedTimes][n][n],w2[RepeatedTimes][n][n],p[RepeatedTimes][m][n],q[RepeatedTimes][m][n],d1[RepeatedTimes][n][n],d2[RepeatedTimes][n][n];



float SIGN(float a,float b)//Singal function
{
	int o;
	if (b>0) o=1;
	if (b<0) o=-1;
	if(b==0)  o=0;
	return o*a;
}
float FMAX(float a,float b)//the maximum of float
{
	if(a<b)  a=b;
	return a;
}
float IMIN(float a,float b)//the minimum of float
{
	if(a>b)  a=b;
	return a;
}
float pythag(float a, float b)//the distance between (a,b) and (0,0)
{
	return sqrt(a*a+b*b);
}

__inline int svd(float a_INOUT[m][n], float v_INOUT[n][n], float w_INOUT[n]) // svd in-out arguments
{
	int flag,i,its = 0,j,jj,k,l,nm;
	float anorm=0,c,f,g,h,s,scale,x,y,z,*rv1;
	float rv2[n];

	/*,;
	float v[n][n],w2[n][n],p[m][n],q[m][n],d1[n][n],d2[n][n];*/
	float t[n],r[m][n];

	for(i=0;i<n;i++) 
		for(j=0;j<n;j++)
			v_INOUT[i][j]=0;
	for(i=0;i<n;i++)
		w_INOUT[i]=0,t[i]=0;
	for(i=0;i<n;i++)
		rv2[i]=0;

	rv1=rv2;
	g=scale=anorm=0.0; //Householder reduction to bidiagonal form.
	for (i=0;i<n;i++)
	{
		l=i+1;
		rv1[i]=scale*g;
		g=s=scale=0.0;
		if (i < m)
		{
			for (k=i;k<m;k++) scale += fabs(a_INOUT[k][i]);
			if (scale)
			{
				for (k=i;k<m;k++)
				{
					a_INOUT[k][i] /= scale;
					s += a_INOUT[k][i]*a_INOUT[k][i];
				}
				f=a_INOUT[i][i];
				g = -SIGN(sqrt(s),f);
				h=f*g-s;
				a_INOUT[i][i]=f-g;
				for (j=l;j<n;j++)
				{
					for (s=0.0,k=i;k<m;k++) s += a_INOUT[k][i]*a_INOUT[k][j];
					f=s/h;
					for (k=i;k<m;k++) a_INOUT[k][j] += f*a_INOUT[k][i];
				}
				for (k=i;k<m;k++) a_INOUT[k][i] *= scale;
			}
		}
		w_INOUT[i]=scale*g;
		g=s=scale=0.0;
		if (i < m && i != n-1)
		{
			for (k=l;k<n;k++) scale += fabs(a_INOUT[i][k]);
			if (scale)
			{
				for (k=l;k<n;k++)
				{
					a_INOUT[i][k] /= scale;
					s += a_INOUT[i][k]*a_INOUT[i][k];
				}
				f=a_INOUT[i][l];
				g = -SIGN(sqrt(s),f);
				h=f*g-s;
				a_INOUT[i][l]=f-g;
				for (k=l;k<n;k++) rv1[k]=a_INOUT[i][k]/h;
				for (j=l;j<m;j++)
				{
					for (s=0.0,k=l;k<n;k++) s += a_INOUT[j][k]*a_INOUT[i][k];
					for (k=l;k<n;k++) a_INOUT[j][k] += s*rv1[k];
				}
				for (k=l;k<n;k++) a_INOUT[i][k] *= scale;
			}
		}
		anorm=FMAX(anorm,(fabs(w_INOUT[i])+fabs(rv1[i])));
	}


	for (i=n-1;i>=0;i--) 

	{ //Accumulation of right-hand transformations.
		if (i < n-1)
		{
			if (g) 
			{
				for (j=l;j<n;j++) //Double division to avoid possible underflow.
					v_INOUT[j][i]=(a_INOUT[i][j]/a_INOUT[i][l])/g;
				for (j=l;j<n;j++) 
				{
					for (s=0.0,k=l;k<n;k++) s += a_INOUT[i][k]*v_INOUT[k][j];
					for (k=l;k<n;k++) v_INOUT[k][j] += s*v_INOUT[k][i];
				}
			}
			for (j=l;j<n;j++) v_INOUT[i][j]=v_INOUT[j][i]=0.0;
		}
		v_INOUT[i][i]=1.0;
		g=rv1[i];
		l=i;
	}


	for (i=IMIN(m,n)-1;i>=0;i--) 
	{ //Accumulation of left-hand transformations.
		l=i+1;
		g=w_INOUT[i];
		for (j=l;j<n;j++) a_INOUT[i][j]=0.0;
		if (g)
		{
			g=1.0/g;
			for (j=l;j<n;j++) 
			{
				for (s=0.0,k=l;k<m;k++) s += a_INOUT[k][i]*a_INOUT[k][j];
				f=(s/a_INOUT[i][i])*g;
				for (k=i;k<m;k++) a_INOUT[k][j] += f*a_INOUT[k][i];
			}
			for (j=i;j<m;j++) a_INOUT[j][i] *= g;
		} 
		else for (j=i;j<m;j++) a_INOUT[j][i]=0.0;
		++a_INOUT[i][i];
	}


	for (k=n-1;k>=0;k--) 
	{ 
		if(its==30){break;}
		//Diagonalization of the bidiagonal form: Loop over
		//singular values, and over allowed iterations. 
		for (its=1;its<=30;its++)
		{
			flag=1;
			for (l=k;l>=0;l--)
			{ 
				//Test for splitting.
				nm=l-1; 
				//Note that rv1[1] is always zero.
				if ((float)(fabs(rv1[l])+anorm) == anorm) 
				{
					flag=0;
					break;
				}
				if ((float)(fabs(w_INOUT[nm])+anorm) == anorm) break;
			}
			if (flag) 
			{
				c=0.0; 
				//Cancellation of rv1[l], if l > 1.
				s=1.0;
				for (i=l;i<=k;i++) 
				{
					f=s*rv1[i];
					rv1[i]=c*rv1[i];
					if ((float)(fabs(f)+anorm) == anorm) break;
					g=w_INOUT[i];
					h=pythag(f,g);
					w_INOUT[i]=h;
					h=1.0/h;
					c=g*h;
					s = -f*h;
					for (j=0;j<m;j++) 
					{
						y=a_INOUT[j][nm];
						z=a_INOUT[j][i];
						a_INOUT[j][nm]=y*c+z*s;
						a_INOUT[j][i]=z*c-y*s;
					}
				}
			}
			z=w_INOUT[k];
			if (l == k) 
			{ 
				//Convergence.
				if (z < 0)
				{ 
					//Singular value is made nonnegative.
					w_INOUT[k] = -z;
					for (j=0;j<n;j++) v_INOUT[j][k] = -v_INOUT[j][k];
				}
				break;
			}
			if (its == 30) 
			{
				printf("erro");			
				break;
			}
			//nrerror//("no convergence in 30 svdcmp iterations");
			x=w_INOUT[l]; 
			//Shift from bottom 2-by-2 minor.
			nm=k-1;
			y=w_INOUT[nm];
			g=rv1[nm];
			h=rv1[k];
			f=((y-z)*(y+z)+(g-h)*(g+h))/(2.0*h*y);
			g=pythag(f,1.0);
			f=((x-z)*(x+z)+h*((y/(f+SIGN(g,f)))-h))/x;
			c=s=1.0; 
			//Next QR transformation:
			for (j=l;j<=nm;j++) 
			{
				i=j+1;
				g=rv1[i];
				y=w_INOUT[i];
				h=s*g;
				g=c*g;
				z=pythag(f,h);
				rv1[j]=z;
				c=f/z;
				s=h/z;
				f=x*c+g*s;
				g = g*c-x*s;
				h=y*s;
				y *= c;
				for (jj=0;jj<n;jj++) 
				{
					x=v_INOUT[jj][j];
					z=v_INOUT[jj][i];
					v_INOUT[jj][j]=x*c+z*s;
					v_INOUT[jj][i]=z*c-x*s;
				}
				z=pythag(f,h);
				w_INOUT[j]=z; 
				//Rotation can be arbitrary if z = 0.
				if (z) 
				{
					z=1.0/z;
					c=f*z;
					s=h*z;
				}
				f=c*g+s*y;
				x=c*y-s*g;
				for (jj=0;jj<m;jj++) 
				{
					y=a_INOUT[jj][j];
					z=a_INOUT[jj][i];
					a_INOUT[jj][j]=y*c+z*s;
					a_INOUT[jj][i]=z*c-y*s;
				}
			}
			rv1[l]=0.0;
			rv1[k]=f;
			w_INOUT[k]=x;
		}

	} 

	for(i=0;i<m;i++)
	{
		for(j=0;j<n;j++)
		{
			r[i][j]=0;

		}
	}

	for(i=0;i<n;i++)
	{
		for(j=i+1;j<n;j++)
		{
			if(w_INOUT[i]<w_INOUT[j])
			{t[i]=w_INOUT[j];
			w_INOUT[j]=w_INOUT[i];
			w_INOUT[i]=t[i];
			for(k=0;k<n;k++)
			{
				r[k][i]=v_INOUT[k][i];
				v_INOUT[k][i]=v_INOUT[k][j];
				v_INOUT[k][j]=r[k][i];
			}
			for(k=0;k<m;k++)
			{
				r[k][i]=a_INOUT[k][i];
				a_INOUT[k][i]=a_INOUT[k][j];
				a_INOUT[k][j]=r[k][i];
			}
			}
		}
	}
	return 0;
}
int main()
{

	char a1[m][n][10];
	int i = 0, j = 0, k = 0, curRepeated = 0;

	double st, diff;

	for(curRepeated=0;curRepeated<RepeatedTimes;curRepeated++)
		for(i=0;i<m;i++)
			for(j=0;j<n;j++)
				a[curRepeated][i][j]=0;

	file1=fopen("Matrixes.dat","r");
	if((file1)==NULL)
	{
		printf("file1 can not open\n");
		exit(1);
	}
	file2=fopen("return.dat","w+");
	if((file2)==NULL)
	{
		printf("file2 can not open\n");
		exit(1);
	}
	for(curRepeated = 0; curRepeated < RepeatedTimes; curRepeated++)
	{
		for(i=0;i<m;i++)
		{
			for(j=0;j<n;j++)
			{
				fscanf(file1,"%s",a1[i][j]);
				a[curRepeated][i][j]=atof(a1[i][j]);
				a_bak[curRepeated][i][j] = a[curRepeated][i][j];
			}
		}
	}
	//for(i = 0; i < m; i++)
	//{
	//	for(j = 0; j < n; j++)
	//	{
	//		printf("%f\t", a[i][j]);
	//	}
	//	printf("\r\n");
	//}

	// Loading complete
	// About 250 lines of codes is doing SVD


	st = clock();

	for(curRepeated = 0;curRepeated<RepeatedTimes;curRepeated++)
	{
		svd(a[curRepeated], v[RepeatedTimes], w[RepeatedTimes]);
	}
	diff = (double)(clock()-st)/CLOCKS_PER_SEC;
	// 0.023s in Windows

	printf("Function svd() consumes %fs.\r\n", diff);

	// Print the final result
	// q = a * w * v

	for(curRepeated = 0;curRepeated<RepeatedTimes;curRepeated++)
	{
		for(i=0;i<m;i++) 
		{ 
			fprintf(file2,"\n");  
			for(j=0;j<n;j++)
				fprintf(file2,"%f    ",a[curRepeated][i][j]);
		}
		fprintf(file2,"\n"); 
		fprintf(file2,"\n"); 
		for(i=0;i<n;i++)
		{
			fprintf(file2,"w[%d]= %f      " ,i,w[curRepeated][i]);
		}
		fprintf(file2,"\n");  
		fprintf(file2,"\n"); 
		for(i=0;i<n;i++) 
		{ 
			fprintf(file2,"\n");  
			for(j=0;j<n;j++)
				fprintf(file2,"%f    ",v[curRepeated][i][j]);
		}
		fprintf(file2,"\n");  
		fprintf(file2,"\n");  


		for(i=0;i<n;i++)
		{
			for(j=0;j<n;j++)
			{
				if(i==j)
					w2[curRepeated][i][j]=w[curRepeated][i];
				else 
					w2[curRepeated][i][j]=0;
			}
		}

		for(i=0;i<m;i++)
		{
			for(j=0;j<n;j++)
			{
				p[curRepeated][i][j]=0;
				q[curRepeated][i][j]=0;
			}
		}

		for(i=0;i<m;i++){
			for(j=0;j<n;j++){
				for (k=0;k<n;k++)
				{
					p[curRepeated][i][j]+=a[curRepeated][i][k]*w2[RepeatedTimes][k][j];
				}}}
		for(i=0;i<m;i++){
			for(j=0;j<n;j++){
				for (k=0;k<n;k++)
				{
					q[curRepeated][i][j]+=p[curRepeated][i][k]*v[curRepeated][j][k];
				}}}
		for(i=0;i<m;i++) 
		{  
			fprintf(file2,"\n");  
			for(j=0;j<n;j++)
			{
				fprintf(file2,"%f    ",q[curRepeated][i][j]);
				diffBetween_a_a_bak += fabs(q[curRepeated][i][j] - a_bak[curRepeated][i][j]);
			}
		}
		fprintf(file2,"\n");  


		for(i=0;i<n;i++)
			for(j=0;j<n;j++)
			{
				d1[curRepeated][i][j]=0;
				d2[curRepeated][i][j]=0;
			}
			for(i=0;i<n;i++)
			{
				for(j=0;j<n;j++)
				{
					for(k=0;k<m;k++)
					{
						d1[curRepeated][i][j]+=a[curRepeated][k][i]*a[curRepeated][k][j];
					}
				}
			}
			for(i=0;i<n;i++)
				for(j=0;j<n;j++)
					for(k=0;k<n;k++)
					{
						d2[curRepeated][i][j]+=v[curRepeated][k][i]*v[curRepeated][k][j];
					}
					for(i=0;i<n;i++) 
					{  
						fprintf(file2,"\n");  
						for(j=0;j<n;j++)
							fprintf(file2,"%f    ",d1[curRepeated][i][j]);
					}
					fprintf(file2,"\n");  
					for(i=0;i<n;i++) 
					{  
						fprintf(file2,"\n");  
						for(j=0;j<n;j++)
							fprintf(file2,"%f    ",d2[curRepeated][i][j]);
					}
					fprintf(file2,"\n");  
	}

	printf("The difference between a and a_bak is: %f.\r\n", diffBetween_a_a_bak);
	return 0;
}

