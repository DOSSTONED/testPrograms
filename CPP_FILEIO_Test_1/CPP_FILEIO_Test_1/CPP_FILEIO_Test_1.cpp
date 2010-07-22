// CPP_FILEIO_Test_1.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"


#include<iostream>
#include<string>
#include<fstream>

using namespace std;




int _tmain(int argc, _TCHAR* argv[])
{
	ifstream file_in("G:\\HIV-1-NCBI.fsa");
	if (!file_in) {
		cout << "File could not be opened.";
		return -1;
	}
	ofstream file_out("G:\\a.txt");
	if (!file_out) {
		cout << "File could not be opened.";
		return -1;
	}
	string textline,a;

	//b=getline(file_in,textline);
	while(1)
	{
		getline(file_in,textline);
		if (textline.find("Locus")!=string::npos) {
			string m=" ";
			int last=textline.find_first_of(m,12);
			file_out<<textline.substr(12,last-11)<<" ";
		}
		if (textline.find("/country")!=string::npos) {
			file_out<<textline<<" ";
		}
		if (textline.find("/gene")!=string::npos) {
			file_out<<textline<<" ";
		}
		if (textline.find("ORIGIN")!=string::npos) {
			while(1) {
				getline(file_in,textline);
				if (textline.find("//")==string::npos)
					break;
				file_out<<textline<<endl;
			}
			//b=getline(file_in,textline);
		}
		
		//if(b!=NULL)
		//	delete b;

		
	}
	file_in.close();
		file_out.close();
	return 0;
}

