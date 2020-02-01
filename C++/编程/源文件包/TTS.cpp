// TTS.cpp : Defines the entry point for the console application.
//

#include <stdafx.h>
#include "sapi.h"
//#include "sphelper.h"
#include <iostream>
#include <sstream>
#include <string>
using namespace std;
int main(int argc, char* argv[])
{	
    ISpVoice * pVoice = NULL;
	
    if (FAILED(::CoInitialize(NULL)))
        return FALSE;
	
	
	///////////////////////////////////////////////
	
	/*
	HRESULT                             hr = S_OK;
	CComPtr<ISpObjectToken>             cpVoiceToken;
	CComPtr<IEnumSpObjectTokens>        cpEnum;
	CComPtr<ISpVoice>                   cpVoice;
	ULONG                               ulCount = 0;
	
	  // Create the SAPI voice
	  if(SUCCEEDED(hr))
	  hr = cpVoice.CoCreateInstance( CLSID_SpVoice ); 
	  
		
		  //Enumerate the available voices 
		  if(SUCCEEDED(hr))
		  hr = SpEnumTokens(SPCAT_VOICES, NULL, NULL, &cpEnum);
		  
			//Get the number of voices
			if(SUCCEEDED(hr))
			hr = cpEnum->GetCount(&ulCount);
			
			  // Obtain a list of available voice tokens, set the voice to the token, and call Speak
			  while (SUCCEEDED(hr) && ulCount -- )
			  {
			  cpVoiceToken.Release();
			  if(SUCCEEDED(hr))
			  hr = cpEnum->Next( 1, &cpVoiceToken, NULL );
			  printf("%c\n",cpVoiceToken);	
			  if(SUCCEEDED(hr))
			  hr = cpVoice->SetVoice(cpVoiceToken);
			  
				if(SUCCEEDED(hr))
				hr = cpVoice->Speak( L"How are you?", SPF_DEFAULT, NULL); 
				}
				//pVoice->Release();
				//pVoice = NULL;//加入这两行之后，会有莫名其妙的错误
	*/
	
	//////////////////////////////////////////////////
	
	/**/
	int Status=1;
	//WCHAR *a = L"Don't speak English";
	WCHAR *String;
	String=new WCHAR[1000];
	short Volume=50;
	
	
	//printf(L"%d",String);
	HRESULT hr = CoCreateInstance(CLSID_SpVoice, NULL, CLSCTX_ALL, IID_ISpVoice, (void **)&pVoice);
	if( SUCCEEDED( hr ) )
	{
		
		while(Status==1)
		{
			//printf("Please input the volume:\n");
			//std::cin>>Volume;//scanf("%d",Volume);
			//hr = pVoice->SetVolume(Volume);
			printf("Please input the words:\n");
			hr = pVoice->Speak(L"Please input the words", SPF_DEFAULT, NULL );
			//getline(wcin,String);
			wscanf(L"%[^\n]",String);
			/*
			if(String==L"exit")
			{
			Status=0;
			exit(0);
			//break;
			}
			*/
			hr = pVoice->Speak(String, SPF_DEFAULT, NULL );
			printf("Speak is over.\n");
			hr = pVoice->Speak(L"Speak is over.", SPF_DEFAULT, NULL );
			//pVoice->Release();
			//pVoice = NULL;
		}
		//Status=0;
	}
	/**/
	
	////////////////////////////////////////////
	
    ::CoUninitialize();
    return TRUE;
}
