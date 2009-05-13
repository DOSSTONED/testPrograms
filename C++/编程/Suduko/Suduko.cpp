#include <iostream>
using namespace std;

///////////////////////////////////



const int Global_Suduko_Row = 9;
const int Global_Suduko_Col = 9;
const int Global_Suduko_Lay = 10;
int Global_Suduko[Global_Suduko_Row][Global_Suduko_Col][Global_Suduko_Lay];
int Global_row = 0 , Global_col = 0 , Global_i = 0 , Global_j = 0 , Global_k = 0 , Global_l = 0 , Global_partr = 0 , Global_partc = 0 , Global_sum = 0 ;

///////////////////////////////////
//Check the filled Suduko

int CheckSuduko(int CheckSuduko[][Global_Suduko_Col][Global_Suduko_Lay])
{
	for( Global_row = 0 ; Global_row < Global_Suduko_Row ; Global_row++ )
	{
		for( Global_col = 0 ; Global_col < Global_Suduko_Col ; Global_col++ )
		{
			
			for( Global_i = 0 ; Global_i < Global_Suduko_Row ; Global_i++ )//CheckCol
			{
				if( ( Global_row != Global_i ) && ( CheckSuduko[Global_row][Global_col][0] == CheckSuduko[Global_i][Global_col][0] ) )
				{
					return -1;
				}
			}
			
			for( Global_j = 0 ; Global_j < Global_Suduko_Col ; Global_j++ )//CheckRow
			{
				if( ( Global_col != Global_j ) && ( CheckSuduko[Global_row][Global_col][0] == CheckSuduko[Global_row][Global_j][0] ) )
				{
					return -1;
				}
			}
			
			Global_partr = Global_row / 3 ;//CheckOther
			Global_partc = Global_col / 3 ;
			for( Global_i = 0 ; Global_i < 3 ; Global_i++ )
			{
				for( Global_j = 0 ; Global_j < 3 ; Global_i++ )
				{
					if( ( Global_row != Global_partr + Global_i ) && ( Global_col != Global_partc + Global_j ) && ( CheckSuduko[Global_row][Global_col][0] == CheckSuduko[Global_partr + Global_i][Global_partc + Global_j][0] ) )
						return -1;
				}
			}
			
		}
	}
	return 0;
}


///////////////////////////////////

///////////////////////////////////
//	Fill


int Fill(int Fill[][Global_Suduko_Col][Global_Suduko_Lay])
{
	for( Global_row = 0 ; Global_row < Global_Suduko_Row ; Global_row++ )
	{
		for( Global_col = 0 ; Global_col < Global_Suduko_Col ; Global_col++ )
		{
			while( Fill[Global_row][Global_col][0] == 0 )
			{
				
				//	Fill the blank , just fill
				{
					Global_sum = 0 ;
					for( Global_k = 1 ; Global_k < Global_Suduko_Lay ; Global_k++ )
					{
						Global_sum = Global_sum + Fill[Global_row][Global_col][Global_k] ;
					}
					if( Global_sum == 1 )
					{
						for( Global_k = 1 ; Global_k < Global_Suduko_Lay ; Global_k++ )
						{
							if( Fill[Global_row][Global_col][Global_k] == 1 )
							{
								Fill[Global_row][Global_col][0] = Global_k ;
								Fill[Global_row][Global_col][Global_k] = 0 ;
							}
						}
					}
				}
				// Fill by row
				{
					for( Global_k = 1 ; Global_k < 10 ; Global_k++ )
					{
						Global_sum = 0 ;
						for( Global_j = 0 ; Global_j < Global_Suduko_Col ; Global_j++ )
						{
							Global_sum = Global_sum + Fill[Global_row][Global_j][Global_k] ;
						}
						if( Global_sum == 1 )
						{
							for( Global_j = 1 ; Global_j < Global_Suduko_Col ; Global_j++ )
							{
								if( Fill[Global_row][Global_j][Global_k] == 1 )
								{
									Fill[Global_row][Global_j][0] = Global_k ;
									for( Global_l = 1 ; Global_l < 10 ; Global_l++ )
									{
										Fill[Global_row][Global_j][Global_l] = 0 ;
									}
								}
							}
						}
					}
				}
			}
		}
	}
	return 0;
}


///////////////////////////////////

///////////////////////////////////



///////////////////////////////////

///////////////////////////////////



///////////////////////////////////

///////////////////////////////////





///////////////////////////////////

///////////////////////////////////



int main(int argc,int *argv[])
{
	int main_row = 0 , main_col = 0 , main_k = 0;
	for( main_row = 0 ; main_row < Global_Suduko_Row ; main_row++ )
	{
		for( main_col = 0 ; main_col < Global_Suduko_Col ; main_col++ )
		{
			cin>>Global_Suduko[main_row][main_col][0];
			if( Global_Suduko[main_row][main_col][0] != 0 )
			{
				for( Global_k = 1 ; Global_k < 10 ; Global_k++ )
				{
					Global_Suduko[main_row][main_col][Global_k] = 0 ;
				}
			}
		}
	}
	
	
	cout<<"\n\n\nYour Suduko is:\n\n";
	
	
	for( main_row = 0 ; main_row < Global_Suduko_Row ; main_row++ )
	{
		for( main_col = 0 ; main_col < Global_Suduko_Col ; main_col++ )
		{
			cout<<Global_Suduko[main_row][main_col][0]<<" ";
		}
		cout<<endl;
	}
	
	return 0;
}



///////////////////////////////////