// CPP_File_IO.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <fstream>
#include <iostream>
#include <string>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	fstream fs("D:\\tset.txt" , ios::in | ios::out | ios::app); // app stands for append!!
	// Write to the file.
	fs << "Writing to a basic_fstream object...12421" << endl;
	fs.close();

	// Dump the contents of the file to cout.
	fs.open("D:\\tset.txt", ios::in);
	cout << fs.rdbuf();

	fs.close();

	return 0;
}


