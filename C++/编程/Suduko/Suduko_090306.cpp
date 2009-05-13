#include <iostream>
using namespace std;

///////////////////////////////////


int Global_Suduko[9][9][10];
int Global_row = 0 , Global_col = 0 , Global_i = 0 , Global_j = 0 , Global_k = 0 , Global_l = 0 , Global_partr = 0 , Global_partc = 0 , Global_sum = 0 ;

///////////////////////////////////
//Check the filled Suduko

int CheckSuduko(int CheckSuduko[][9][10])
{
	for( Global_row = 0 ; Global_row < 9 ; Global_row++ )
	{
		for( Global_col = 0 ; Global_col < 9 ; Global_col++ )
		{
			
			for( Global_i = 0 ; Global_i < 9 ; Global_i++ )//CheckCol
			{
				if( ( Global_row != Global_i ) && ( CheckSuduko[Global_row][Global_col][0] == CheckSuduko[Global_i][Global_col][0] ) )
				{
					return -1;
				}
			}
			
			for( Global_j = 0 ; Global_j < 9 ; Global_j++ )//CheckRow
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
//  Clean nonuseful number
int Clean(int Clean[][9][10])
{
	//Clean current
	for( Global_row = 0 ; Global_row < 9 ; Global_row++ )
	{
		for( Global_col = 0 ; Global_col < 9 ; Global_col++ )
		{
			if( Clean[Global_row][Global_col][0] )
			{
				//Clean
				{
					for( Global_l = 1 ; Global_l < 10 ; Global_l++ )
						Clean[Global_row][Global_col][Global_l] = 0 ;
				}
				
				
				
				
				//Clean row
				{
					
				}
				
				
				//Clean col
				{
					for( Global_j = 0 ; Global_j < 9 ; Global_j++ )
					{
						Clean[ Global_j ][ Global_col ][ (Clean[Global_row][Global_col][0]) ] = 0 ;
					}
				}
				
				
				//Clean sqr
				{
				}
			}
			
		}
	}
	
	return 0;
}


///////////////////////////////////
//	Fill


int Fill(int Fill[][9][10])
{
	for( Global_row = 0 ; Global_row < 9 ; Global_row++ )
	{
		for( Global_col = 0 ; Global_col < 9 ; Global_col++ )
		{
			if( Fill[Global_row][Global_col][0] == 0 )
			{
				
				//	Fill the blank , just fill
				{
					Global_sum = 0 ;
					for( Global_k = 1 ; Global_k < 9 ; Global_k++ )
					{
						Global_sum = Global_sum + Fill[Global_row][Global_col][Global_k] ;
					}
					if( Global_sum == 1 )
					{
						for( Global_k = 1 ; Global_k < 9 ; Global_k++ )
						{
							if( Fill[Global_row][Global_col][Global_k] == 1 )
							{
								Fill[Global_row][Global_col][0] = Global_k ;
								Fill[Global_row][Global_col][Global_k] = 0 ;
							}
						}
					}
				}
				Global_k = 1 ;
				
				// Fill by row
				{
					for( Global_k = 1 ; Global_k < 10 ; Global_k++ )
					{
						Global_sum = 0 ;
						for( Global_j = 0 ; Global_j < 9 ; Global_j++ )
						{
							Global_sum = Global_sum + Fill[Global_row][Global_j][Global_k] ;
						}
						if( Global_sum == 1 )
						{
							for( Global_j = 0 ; Global_j < 9 ; Global_j++ )
							{
								if( Fill[Global_row][Global_j][Global_k] == 1 )
								{
									Fill[Global_row][Global_j][0] = Global_k ;
									for( Global_l = 1 ; Global_l < 10 ; Global_l++ )  //Clean
									{
										Fill[Global_row][Global_j][Global_l] = 0 ;
									}
								}
							}
						}
					}
				}
				Global_k = 1 ;
				Global_j = 0 ;
				Global_l = 1 ;
				// Fill by col
				{
					for( Global_k = 1 ; Global_k < 10 ; Global_k++ )
					{
						Global_sum = 0 ;
						for( Global_i = 0 ; Global_i < 9 ; Global_i++ )
						{
							Global_sum = Global_sum + Fill[Global_i][Global_col][Global_k] ;
						}
						if( Global_sum == 1 )
						{
							for( Global_i = 0 ; Global_i < 9 ; Global_i++ )
							{
								if( Fill[Global_i][Global_col][Global_k] == 1 )
								{
									Fill[Global_i][Global_col][0] = Global_k ;
									for( Global_l = 1 ; Global_l < 10 ; Global_l++ )  //Clean
									{
										Fill[Global_i][Global_col][Global_l] = 0 ;
									}
								}
							}
						}
					}
				}
				Global_k = 1 ;
				Global_i = 0 ;
				Global_l = 1 ;
				// Fill by square
				{
					int FillSqrCol = Global_col % 3 , FillSqrRow = Global_row % 3 ; //These two indicate the coordinates(x,y) moudle 3
					Global_i = 0 ;
					Global_j = 0 ;
					for( Global_k = 1 ; Global_k < 10 ; Global_k++ )
					{
						Global_sum = 0 ;
						for( Global_i = 0 ; Global_i < 3 ; Global_i++ )
						{
							for( Global_j = 0 ; Global_j < 3 ; Global_j++ )
							{
								Global_sum = Global_sum + Fill[ FillSqrRow + Global_i ][ FillSqrCol + Global_j ][ Global_k ] ;
							}
						}
						if( Global_sum == 1 )
						{
							for( Global_i = 0 ; Global_i < 3 ; Global_i++ )
							{
								for( Global_j = 0 ; Global_j < 3 ; Global_j++ )
								{
									if( Fill[ FillSqrRow + Global_i][ FillSqrCol + Global_j][ Global_k ] == 1 )
									{
										Fill[ FillSqrRow + Global_i][ FillSqrCol + Global_j][0] = Global_k ;
										//for( Global_l = 1 ; Global_l < 10 ; Global_l++ )// Clean
										//{
										//	Fill[Global_i][Global_j][Global_l] = 0 ;
										//}
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
	for( main_row = 0 ; main_row < 9 ; main_row++ )
	{
		for( main_col = 0 ; main_col < 9 ; main_col++ )
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
	
	
	for( main_row = 0 ; main_row < 9 ; main_row++ )
	{
		for( main_col = 0 ; main_col < 9 ; main_col++ )
		{
			cout<<Global_Suduko[main_row][main_col][0]<<" ";
		}
		cout<<endl;
	}


	cout<<"Cracking......\n";

	int main_i ;
	for( main_i = 0 ; main_i < 100 ; main_i++ )
	{
		//Clean ( Global_Suduko );
		Fill( Global_Suduko );
	}
	

	cout<<"The answer is:\n";
	for( main_row = 0 ; main_row < 9 ; main_row++ )
	{
		for( main_col = 0 ; main_col < 9 ; main_col++ )
		{
			cout<<Global_Suduko[main_row][main_col][0]<<" ";
		}
		cout<<endl;
	}
	return 0;
}



///////////////////////////////////