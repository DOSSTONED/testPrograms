// TTS.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "sapi.h"
#include "sphelper.h"
#include <iostream>
//#include <string>
//using namespace std;
int main(int argc, char* argv[])
{	
    ISpVoice * pVoice = NULL;
	
    if (FAILED(::CoInitialize(NULL)))
        return FALSE;
	//int Token[10];
	int i=0;
	///////////////////////////////////////////////
	
	/**/
	HRESULT                             hr = S_OK;
	CComPtr<ISpObjectToken>             cpVoiceToken,Token;
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
			
			printf("There are voices avliable:\n");
			  // Obtain a list of available voice tokens, set the voice to the token, and call Speak
			  while (SUCCEEDED(hr) && ulCount -- )
			  {
			  cpVoiceToken.Release();
			  if(SUCCEEDED(hr))
			  hr = cpEnum->Next( 1, &cpVoiceToken, NULL );
			  
			  printf("%x\n",cpVoiceToken);	
			  
			  if(SUCCEEDED(hr))
			  hr = cpVoice->SetVoice(cpVoiceToken);
			  
				if(SUCCEEDED(hr))
				hr = cpVoice->Speak( L"How are you?", SPF_DEFAULT, NULL); 
				hr = cpVoice->Speak( L"���!", SPF_DEFAULT, NULL);
				}
				//pVoice->Release();
				//pVoice = NULL;//����������֮�󣬻���Ī������Ĵ���
	/**/
	
	//////////////////////////////////////////////////
	
	/**/
	int Status=1;
	WCHAR *a = L"Don't speak English";
	WCHAR *String;
	String=new WCHAR[1000];
	//int *Token;
	//short Volume=50;
	
	
	//printf(L"%d",String);
	HRESULT hr1 = CoCreateInstance(CLSID_SpVoice, NULL, CLSCTX_ALL, IID_ISpVoice, (void **)&pVoice);
	if( SUCCEEDED( hr1 ) )
	{
///////////////////////////
		/*
		printf("Input the numbers before:\n");
		scanf("%x",Token);
		hr1 = pVoice->SetVoice(Token);
*/
  ///////////////////////////

		//printf("Please input the volume:\n");
		//std::cin>>Volume;//scanf("%d",Volume);
		//hr = pVoice->SetVolume(Volume);//����������֮��֪��Ϊʲô��ʹString����ʧЧ~~

		printf("Please input the words:\n");
		hr1 = pVoice->Speak(L"Please input the words", SPF_DEFAULT, NULL );//����ʹ��ʱΪhr = pVoice->Speak(L"Please input the words", SPF_DEFAULT, NULL );
		//while(Status==1)
		//{
			wscanf(L"%[^\n]",String);
			/*
			if(String==L"exit")
			{
			Status=0;
			exit(0);
			//break;
			}
			*/
			hr1 = pVoice->Speak(String, SPF_DEFAULT, NULL );//����ʹ��ʱΪhr = pVoice->Speak(String, SPF_DEFAULT, NULL );
			//fflush(0);
			printf("Speak is over.\n");
			hr1 = pVoice->Speak(L"Speak is over.", SPF_DEFAULT, NULL );//����ʹ��ʱΪhr = pVoice->Speak(L"Speak is over.", SPF_DEFAULT, NULL );
			//pVoice->Release();
			//pVoice = NULL;
		//}//ʹ��ѭ��֮��ֻ�ܽ���һ���ַ������룬�ڶ���֮������ʧЧ��~~�����http://blog.chinaunix.net/u/15262/showart_382615.html
		//Status=0;
	}
	/**/
	
	////////////////////////////////////////////
	
    ::CoUninitialize();
    return TRUE;
}