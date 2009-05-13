#include <CSTDIO>    
#include <VECTOR>    
#include <ALGORITHM>    
enum{SIZE=81};   
unsigned int Data[SIZE]={//未解棋盘数据    
	0 , 9 , 0 ,  0 , 6 , 0 ,  5 , 4 , 8 ,   
	4 , 0 , 3 ,  0 , 8 , 0 ,  9 , 0 , 0 ,   
	8 , 6 , 5 ,  4 , 7 , 9 ,  1 , 2 , 3 ,   
	0 , 5 , 6 ,  3 , 9 , 0 ,  4 , 0 , 1 ,   
	1 , 4 , 0 ,  0 , 5 , 0 ,  2 , 0 , 0 ,   
	0 , 0 , 0 ,  0 , 4 , 1 ,  0 , 0 , 0 ,   
	0 , 0 , 0 ,  8 , 2 , 0 ,  6 , 1 , 0 ,   
	0 , 0 , 0 ,  0 , 3 , 0 ,  0 , 0 , 4 ,   
	5 , 8 , 0 ,  9 , 1 , 0 ,  0 , 0 , 0 };   
const int temp[9] = { 1 , 2 , 3, 4, 5, 6, 7, 8, 9};   
struct Item   
{   
	int   data;   
	std::vector<INT> other;   
	Item():data(0),other(temp,temp+9){}   
	inline bool operator==(int x)   
	{   
		return x==data?true:false;   
	}   
	inline Item& operator=(const Item& src)   
	{   
		data = src.data ;   
		other = src.other;   
		return (*this);   
	};   
	inline Item& operator=(int x){   
		data = x ;   
		std::copy(temp,temp+sizeof(temp)/sizeof(temp[0]) , other.begin());   
		return (*this);   
	};   
	void test(size_t x ){   
		if( other.size() == 2  )   
		data = other[x];   
	}   
	inline operator int(){return data;}   
};   
struct GroupInfo{   
	const int Group1,Group2,Group3;   
	GroupInfo(int g1,int g2,int g3):Group1(g1),Group2(g2),Group3(g3){}   
	inline bool operator==(GroupInfo& src){   
		return ((Group1|Group2|Group3)&(src.Group1|src.Group2|src.Group3))?true:false;   
	}   
};    

GroupInfo Group[SIZE]={   
	GroupInfo( 1<<1 , 1<<10 , 1<<19) ,GroupInfo( 1<<1 , 1<<11 , 1<<19) ,GroupInfo( 1<<1 , 1<<12 , 1<<19) ,GroupInfo( 1<<1 , 1<<13 , 1<<20) ,GroupInfo( 1<<1 , 1<<14 , 1<<20) ,GroupInfo( 1<<1 , 1<<15 , 1<<20) ,GroupInfo( 1<<1 , 1<<16 , 1<<21) ,GroupInfo( 1<<1 , 1<<17 , 1<<21) ,GroupInfo( 1<<1 , 1<<18 , 1<<21) ,   
	GroupInfo( 1<<2 , 1<<10 , 1<<19) ,GroupInfo( 1<<2 , 1<<11 , 1<<19) ,GroupInfo( 1<<2 , 1<<12 , 1<<19) ,GroupInfo( 1<<2 , 1<<13 , 1<<20) ,GroupInfo( 1<<2 , 1<<14 , 1<<20) ,GroupInfo( 1<<2 , 1<<15 , 1<<20) ,GroupInfo( 1<<2 , 1<<16 , 1<<21) ,GroupInfo( 1<<2 , 1<<17 , 1<<21) ,GroupInfo( 1<<2 , 1<<18 , 1<<21) ,   
	GroupInfo( 1<<3 , 1<<10 , 1<<19) ,GroupInfo( 1<<3 , 1<<11 , 1<<19) ,GroupInfo( 1<<3 , 1<<12 , 1<<19) ,GroupInfo( 1<<3 , 1<<13 , 1<<20) ,GroupInfo( 1<<3 , 1<<14 , 1<<20) ,GroupInfo( 1<<3 , 1<<15 , 1<<20) ,GroupInfo( 1<<3 , 1<<16 , 1<<21) ,GroupInfo( 1<<3 , 1<<17 , 1<<21) ,GroupInfo( 1<<3 , 1<<18 , 1<<21) ,   
	GroupInfo( 1<<4 , 1<<10 , 1<<22) ,GroupInfo( 1<<4 , 1<<11 , 1<<22) ,GroupInfo( 1<<4 , 1<<12 , 1<<22) ,GroupInfo( 1<<4 , 1<<13 , 1<<23) ,GroupInfo( 1<<4 , 1<<14 , 1<<23) ,GroupInfo( 1<<4 , 1<<15 , 1<<23) ,GroupInfo( 1<<4 , 1<<16 , 1<<24) ,GroupInfo( 1<<4 , 1<<17 , 1<<24) ,GroupInfo( 1<<4 , 1<<18 , 1<<24) ,   
	GroupInfo( 1<<5 , 1<<10 , 1<<22) ,GroupInfo( 1<<5 , 1<<11 , 1<<22) ,GroupInfo( 1<<5 , 1<<12 , 1<<22) ,GroupInfo( 1<<5 , 1<<13 , 1<<23) ,GroupInfo( 1<<5 , 1<<14 , 1<<23) ,GroupInfo( 1<<5 , 1<<15 , 1<<23) ,GroupInfo( 1<<5 , 1<<16 , 1<<24) ,GroupInfo( 1<<5 , 1<<17 , 1<<24) ,GroupInfo( 1<<5 , 1<<18 , 1<<24) ,   
	GroupInfo( 1<<6 , 1<<10 , 1<<22) ,GroupInfo( 1<<6 , 1<<11 , 1<<22) ,GroupInfo( 1<<6 , 1<<12 , 1<<22) ,GroupInfo( 1<<6 , 1<<13 , 1<<23) ,GroupInfo( 1<<6 , 1<<14 , 1<<23) ,GroupInfo( 1<<6 , 1<<15 , 1<<23) ,GroupInfo( 1<<6 , 1<<16 , 1<<24) ,GroupInfo( 1<<6 , 1<<17 , 1<<24) ,GroupInfo( 1<<6 , 1<<18 , 1<<24) ,   
	GroupInfo( 1<<7 , 1<<10 , 1<<25) ,GroupInfo( 1<<7 , 1<<11 , 1<<25) ,GroupInfo( 1<<7 , 1<<12 , 1<<25) ,GroupInfo( 1<<7 , 1<<13 , 1<<26) ,GroupInfo( 1<<7 , 1<<14 , 1<<26) ,GroupInfo( 1<<7 , 1<<15 , 1<<26) ,GroupInfo( 1<<7 , 1<<16 , 1<<27) ,GroupInfo( 1<<7 , 1<<17 , 1<<27) ,GroupInfo( 1<<7 , 1<<18 , 1<<27) ,   
	GroupInfo( 1<<8 , 1<<10 , 1<<25) ,GroupInfo( 1<<8 , 1<<11 , 1<<25) ,GroupInfo( 1<<8 , 1<<12 , 1<<25) ,GroupInfo( 1<<8 , 1<<13 , 1<<26) ,GroupInfo( 1<<8 , 1<<14 , 1<<26) ,GroupInfo( 1<<8 , 1<<15 , 1<<26) ,GroupInfo( 1<<8 , 1<<16 , 1<<27) ,GroupInfo( 1<<8 , 1<<17 , 1<<27) ,GroupInfo( 1<<8 , 1<<18 , 1<<27) ,   
	GroupInfo( 1<<9 , 1<<10 , 1<<25) ,GroupInfo( 1<<9 , 1<<11 , 1<<25) ,GroupInfo( 1<<9 , 1<<12 , 1<<25) ,GroupInfo( 1<<9 , 1<<13 , 1<<26) ,GroupInfo( 1<<9 , 1<<14 , 1<<26) ,GroupInfo( 1<<9 , 1<<15 , 1<<26) ,GroupInfo( 1<<9 , 1<<16 , 1<<27) ,GroupInfo( 1<<9 , 1<<17 , 1<<27) ,GroupInfo( 1<<9 , 1<<18 , 1<<27)    
};   
bool AI(std::vector<ITEM>& game)   
{   
	bool bMoveflag = false;   
	for(size_t x = 0 ; x < game.size() ; ++x ){   
		if( 0 != game[x].data ){//依次检查每个位置    
			game[x].other.resize(0);   
			continue;   
		}   
		//当前位置没有数字    
		std::vector<INT> vTemp;   
		for(int i = 0 ; i < 81 ; ++i )   
		if( Group[x]==Group[i] )   
		vTemp.push_back ( game[i].data );   
		;   
		vTemp.erase( std::remove(vTemp.begin(),vTemp.end() , 0 ) , vTemp.end() );    

		//移除同组已经出现的数字    
		for(std::vector<INT>::iterator Iter = vTemp.begin() ; Iter !=vTemp.end() ; ++ Iter )   
		std::replace(game[x].other.begin() , game[x].other.end() , (*Iter) , 0 );   
		game[x].other.erase( std::remove(game[x].other.begin(),game[x].other.end() , 0 ) ,game[x].other.end() );    

		if( ( 1 == game[x].other.size())&&( 0 != game[x].other[0] ) ){   
			game[x].data = game[x].other[0];   
			bMoveflag = true;   
		}   
	}   
	return bMoveflag;   
}    

struct OtherIs2Opt{   
	bool operator()(Item& item)   
	{return ( item.other.size()==2)?true:false;}   
};    

struct testBackOpt   
{   
	bool bBack;   
	testBackOpt():bBack(false){}   
	void operator()(Item& item)   
	{   
		if( ( item.data==0)&&(item.other.size()==0) )   
		bBack = true;   
	}   
};    

bool AdvanceAI(std::vector<ITEM>& game)   
{   
	std::vector<ITEM> Back = game;   
	std::vector<ITEM>::iterator iItem = std::find_if( Back.begin() , Back.end() , OtherIs2Opt() );   
	if( iItem != Back.end() ){   
		for(size_t i = 0 ; i < (*iItem).other.size() ; ++i ){   
			(*iItem).test( i );   
			for( ; AI( Back ) ;);    

			if( std::for_each( Back.begin() , Back.end() , testBackOpt() ).bBack ){//是否结束回滚    
				Back = game;   
				iItem = std::find_if( Back.begin() , Back.end() , OtherIs2Opt() );    
				continue;   
			}    

			if( std::count( Back.begin() , Back.end() , 0 ) ){//判断是否结束    
				if( AdvanceAI( Back ) ){//没有结束，继续下一步递归    
					game = Back ;   
					return true;   
				}   
				Back = game;   
				iItem = std::find_if( Back.begin() , Back.end() , OtherIs2Opt() );    
				continue;   
			}else{//back为结果    
				game = Back ;   
				return true;   
			}   
		}   
	}   
	return false;   
}    

int main(int argc, char* argv[])   
{//初始化棋盘    
	std::vector<ITEM> game(SIZE);   
	std::copy(Data,Data+SIZE ,  game.begin() );   
	for( ; AI( game ) ;);    

	if( std::count( game.begin() , game.end() , 0 ) ){   
		if( !AdvanceAI( game )  )   
		printf("没解出来\n");   
	}   
	for(int x = 0 ; x < 81 ; ++x ){   
		printf(" %d",game[x].data );   
		if( 0 == (x +1)% 9  )   
		printf("\n");   
	}   
	return 0;   
}   

