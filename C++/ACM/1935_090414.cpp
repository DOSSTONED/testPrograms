#include <iostream>
#include <math.h>
using namespace std;
class Jet
{
public:
	int GetCoordinates()
	{
		cin>>x;
		cin>>y;
		cin>>z;
		return 0;
	}

	double Distance ( Jet end )
	{
		return sqrt( double( ( x - end.x ) * ( x - end.x ) + ( y - end.y ) * ( y - end.y ) + ( z - end.z ) * ( z - end.z ) ) + 1 );
	}
	
private:
	double x;
	double y;
	double z;
};
int main()
{
	int number=0,i=0;
	cin>>number;
	Jet *in;
	in = new Jet [ number ];
	for( i = 0 ; i < number ; i++ )
	{
		in[i].GetCoordinates();
	}

	double min=in[0].Distance ( in[ number - 1 ] ) ;
	for( i = 0 ; i < number -1 ; i++ )
	{
		if( min > in[i].Distance ( in[ number - 1 ] ) )
		{
			min = in[i].Distance ( in[ number - 1 ] ) ;
		}
	}
	min=1/min;
	printf("%.4f\n",min);
	return 0;
}

