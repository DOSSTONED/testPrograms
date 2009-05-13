#include <iostream>
using namespace std;
int main()
{
	int n=0,s=0;
	while(cin>>n)
	{
		s=1;
		while(s)
		{
			if(n<50025002)
			{
				n=n+2005;
				s++;
			}
			else
			{
				n=n-5;
				s--;
			}
		}
		cout<<n<<endl;
	}
	return 0;
}
