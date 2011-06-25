#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#include "math.h"

#define m 6
#define n 6


FILE *file1 = NULL;
FILE *file2 = NULL;
float SIGN(float a,float b)
{
	int o;
	if (b>0) o=1;
	if (b<0) o=-1;
	if(b=0)  o=0;
	return o*a;
}
float FMAX(float a,float b)
{
	if(a<b)  a=b;
	return a;
}
float IMIN(float a,float b)
{
	if(a>b)  a=b;
	return a;
}
float pythag(float a, float b)
{
	return sqrt(a*a+b*b);
}
int main()
{
	char a1[m][n][10];
	int flag,i,j,jj,k,l,nm;
	float anorm=0,c,f,g,h,s,scale,x,y,z,*rv1;
	float rv2[n];
	float a[m][n];
	int its = 0;
	float w[n],t[n];
	float v[n][n],w2[n][n],p[m][n],q[m][n],r[m][n],d1[n][n],d2[n][n];
	
	for(i=0;i<m;i++)
		for(j=0;j<n;j++)
			a[i][j]=0;
	for(i=0;i<n;i++) 
		for(j=0;j<n;j++)
			v[i][j]=0;
	for(i=0;i<n;i++)
		w[i]=0,t[i]=0;
	for(i=0;i<n;i++)
		rv2[i]=0;
	file1=fopen("star.dat","r");
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
	for(i=0;i<m;i++)
		for(j=0;j<n;j++)
		{
			fscanf(file1,"%s",a1[i][j]);
			a[i][j]=atof(a1[i][j]);
		}
	for(i = 0; i < m; i++)
	{
		for(j = 0; j < n; j++)
		{
			printf("%f\t", a[i][j]);
		}
		printf("\r\n");
	}

// Loading complete!

	rv1=rv2;
	g=scale=anorm=0.0; //Householder reduction to bidiagonal form.
	for (i=0;i<n;i++)
	{
		l=i+1;
		rv1[i]=scale*g;
		g=s=scale=0.0;
		if (i < m)
		{
			for (k=i;k<m;k++) scale += fabs(a[k][i]);
			if (scale)
			{
				for (k=i;k<m;k++)
				{
					a[k][i] /= scale;
					s += a[k][i]*a[k][i];
				}
				f=a[i][i];
				g = -SIGN(sqrt(s),f);
				h=f*g-s;
				a[i][i]=f-g;
				for (j=l;j<n;j++)
				{
					for (s=0.0,k=i;k<m;k++) s += a[k][i]*a[k][j];
					f=s/h;
					for (k=i;k<m;k++) a[k][j] += f*a[k][i];
				}
				for (k=i;k<m;k++) a[k][i] *= scale;
			}
		}
		w[i]=scale*g;
		g=s=scale=0.0;
		if (i < m && i != n-1)
		{
			for (k=l;k<n;k++) scale += fabs(a[i][k]);
			if (scale)
			{
				for (k=l;k<n;k++)
				{
					a[i][k] /= scale;
					s += a[i][k]*a[i][k];
				}
				f=a[i][l];
				g = -SIGN(sqrt(s),f);
				h=f*g-s;
				a[i][l]=f-g;
				for (k=l;k<n;k++) rv1[k]=a[i][k]/h;
				for (j=l;j<m;j++)
				{
					for (s=0.0,k=l;k<n;k++) s += a[j][k]*a[i][k];
					for (k=l;k<n;k++) a[j][k] += s*rv1[k];
				}
				for (k=l;k<n;k++) a[i][k] *= scale;
			}
		}
		anorm=FMAX(anorm,(fabs(w[i])+fabs(rv1[i])));
	}


	for (i=n-1;i>=0;i--) 

	{ //Accumulation of right-hand transformations.
		if (i < n-1)
		{
			if (g) 
			{
				for (j=l;j<n;j++) //Double division to avoid possible underflow.
					v[j][i]=(a[i][j]/a[i][l])/g;
				for (j=l;j<n;j++) 
				{
					for (s=0.0,k=l;k<n;k++) s += a[i][k]*v[k][j];
					for (k=l;k<n;k++) v[k][j] += s*v[k][i];
				}
			}
			for (j=l;j<n;j++) v[i][j]=v[j][i]=0.0;
		}
		v[i][i]=1.0;
		g=rv1[i];
		l=i;
	}


	for (i=IMIN(m,n)-1;i>=0;i--) 
	{ //Accumulation of left-hand transformations.
		l=i+1;
		g=w[i];
		for (j=l;j<n;j++) a[i][j]=0.0;
		if (g)
		{
			g=1.0/g;
			for (j=l;j<n;j++) 
			{
				for (s=0.0,k=l;k<m;k++) s += a[k][i]*a[k][j];
				f=(s/a[i][i])*g;
				for (k=i;k<m;k++) a[k][j] += f*a[k][i];
			}
			for (j=i;j<m;j++) a[j][i] *= g;
		} 
		else for (j=i;j<m;j++) a[j][i]=0.0;
		++a[i][i];
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
			if ((float)(fabs(w[nm])+anorm) == anorm) break;
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
					g=w[i];
					h=pythag(f,g);
					w[i]=h;
					h=1.0/h;
					c=g*h;
					s = -f*h;
					for (j=0;j<m;j++) 
					{
						y=a[j][nm];
						z=a[j][i];
						a[j][nm]=y*c+z*s;
						a[j][i]=z*c-y*s;
					}
				}
			}
			z=w[k];
			if (l == k) 
			{ 
				//Convergence.
				if (z < 0)
				{ 
					//Singular value is made nonnegative.
					w[k] = -z;
					for (j=0;j<n;j++) v[j][k] = -v[j][k];
				}
				break;
			}
			if (its == 30) 
			{
				printf("erro");			
				break;
			}
			//nrerror//("no convergence in 30 svdcmp iterations");
			x=w[l]; 
			//Shift from bottom 2-by-2 minor.
			nm=k-1;
			y=w[nm];
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
				y=w[i];
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
					x=v[jj][j];
					z=v[jj][i];
					v[jj][j]=x*c+z*s;
					v[jj][i]=z*c-x*s;
				}
				z=pythag(f,h);
				w[j]=z; 
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
					y=a[jj][j];
					z=a[jj][i];
					a[jj][j]=y*c+z*s;
					a[jj][i]=z*c-y*s;
				}
			}
			rv1[l]=0.0;
			rv1[k]=f;
			w[k]=x;
}

} 

for(i=0;i<m;i++)
for(j=0;j<n;j++)
{
	r[i][j]=0;
	
}


for(i=0;i<n;i++)
   for(j=i+1;j<n;j++)
   {
   	   if(w[i]<w[j])
	   {t[i]=w[j];
	       w[j]=w[i];
		   w[i]=t[i];
		   for(k=0;k<n;k++)
		   {
		   r[k][i]=v[k][i];
		   v[k][i]=v[k][j];
		   v[k][j]=r[k][i];
		   }
		   for(k=0;k<m;k++)
		   {
		   r[k][i]=a[k][i];
		   a[k][i]=a[k][j];
		   a[k][j]=r[k][i];
		   }
	   }
   }



   for(i=0;i<m;i++) 
{ 
	fprintf(file2,"\n");  
	for(j=0;j<n;j++)
		fprintf(file2,"%f    ",a[i][j]);
}
   fprintf(file2,"\n"); 
   fprintf(file2,"\n"); 
   for(i=0;i<n;i++)
{
	fprintf(file2,"w[%d]= %f      " ,i,w[i]);
}
	fprintf(file2,"\n");  
	fprintf(file2,"\n"); 
for(i=0;i<n;i++) 
{ 
	fprintf(file2,"\n");  
	for(j=0;j<n;j++)
		fprintf(file2,"%f    ",v[i][j]);
}
	fprintf(file2,"\n");  
	fprintf(file2,"\n");  

	
	for(i=0;i<n;i++)
for(j=0;j<n;j++)
{
	if(i==j)
		w2[i][j]=w[i];
	else 
		w2[i][j]=0;
}
for(i=0;i<m;i++)
for(j=0;j<n;j++)
{
	p[i][j]=0;
	q[i][j]=0;
}

for(i=0;i<m;i++)
for(j=0;j<n;j++)
 for (k=0;k<n;k++)
{
	p[i][j]+=a[i][k]*w2[k][j];
}
for(i=0;i<m;i++)
for(j=0;j<n;j++)
 for (k=0;k<n;k++)
{
	q[i][j]+=p[i][k]*v[j][k];
}
for(i=0;i<m;i++) 
{  
	fprintf(file2,"\n");  
	for(j=0;j<n;j++)
		fprintf(file2,"%f    ",q[i][j]);
}
	fprintf(file2,"\n");  


for(i=0;i<n;i++)
for(j=0;j<n;j++)
{
	d1[i][j]=0;
	d2[i][j]=0;
}
for(i=0;i<n;i++)
for(j=0;j<n;j++)
for(k=0;k<m;k++)
{
d1[i][j]+=a[k][i]*a[k][j];
}
for(i=0;i<n;i++)
for(j=0;j<n;j++)
for(k=0;k<n;k++)
{
d2[i][j]+=v[k][i]*v[k][j];
}
for(i=0;i<n;i++) 
{  
	fprintf(file2,"\n");  
	for(j=0;j<n;j++)
		fprintf(file2,"%f    ",d1[i][j]);
}
	fprintf(file2,"\n");  
	for(i=0;i<n;i++) 
{  
	fprintf(file2,"\n");  
	for(j=0;j<n;j++)
		fprintf(file2,"%f    ",d2[i][j]);
}
	fprintf(file2,"\n");  
return 0;
}

