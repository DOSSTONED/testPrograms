//��ҵ������ȡ���
//����ʱ��Ϊ����ֵ��ᵼ����ѭ������
#include <iostream>
using namespace std;
int main()
{
	const int CardAccount[3]={123,456,789};//���ÿ���
	int CardPassword[3]={123,456,789},Retry=3,Status=0,Choice,i;//�������룬���Դ�������¼״̬��ѡ�񣬵ڼ��ſ�
	double CardRemain[3]={0.0,0.0,0.0},Money=0;//���������ȡ��Ǯ��
	int Account,Password,PasswordConfirm;//��ǰ���������룬ȷ������
Start:system("color 71");//��������ı���ɫ
	  system("mode con cols=55 lines=15");//���ô��ڴ�С
	cout<<"\t\t��ӭ��������ATM\n";
	while(Status==0)
	{
		cout<<"�������ʺţ�";
		cin>>Account;
		for(i=0;i<3;i++)//�ж��ǵڼ��ſ�
			if(Account==CardAccount[i])
				break;//����ɾ��֮�������������ʱ����3�����
		if(Account==CardAccount[i])
		{
			while(Retry>0)
			{
				cout<<"���������룺";
				cin>>Password;
					if(Account==CardAccount[i]&&Password==CardPassword[i])//���������Ӧ
					{
						Status=1;
						break;
					}
					else//���������
					{
						Retry=Retry-1;
						if(Retry>0)
							cout<<"������������ԣ�������"<<Retry<<"�����Ի���\n";
						else
						{
							cout<<"���Ѿ����������Դ��������̿�\n";
							system("color ac");
							system("pause");
							exit(0);
						}
					}
			}
		}
		else
			cout<<"�޴��˺ţ�����������\n";
	}
	system("cls");
	system("color 1e");
	while(1)
	{
		cout<<"��ǰ��¼�˺�Ϊ��"<<Account<<endl;
		cout<<"�����������������q�������������������r����������������\n";
		cout<<"�q���������������Ȼ�ӭ��������ȡ��������������������r\n";
		cout<<"�����������������t�������������������s����������������\n";
		cout<<"����ȡ���Ŀǰ�ṩ���¹��ܣ���������������������������\n"
			<<"��1. ��ѯ����������������������������2. ��ȡ�ֽ�\n"
			<<"��3. ��������������������������������4. �޸����멦\n"
			<<"��9. �˳���������������������������������0. �뿪���Щ�\n";
		cout<<"�t���������������������������������������������������s\n";
		cout<<"��������Ӧ���ֽ��в�����";
		cin>>Choice;
		switch (Choice)
		{
			case 1://��ѯ���
				cout<<"��ǰ��"<<CardRemain[i]<<endl;
				system("pause");
				system("cls");
				break;
			case 2://��ȡ�ֽ�
				cout<<"��������Ҫ��ȡ���ֽ�����";
				cin>>Money;
				if((CardRemain[i]-Money)<0)
				{
					cout<<"\n����ʧ�ܣ����㣬�޷���ȡ";
				}
				else
				{
					CardRemain[i]=CardRemain[i]-Money;
					cout<<"���׳ɹ�����ǰ��"<<CardRemain[i]<<endl;
				}
				system("pause");
				system("cls");
				break;
			case 3://���
				cout<<"��������Ҫ������ֽ�����";
				cin>>Money;
				if(Money<=0)
					cout<<"\n����ʧ�ܣ������ֽ�������Ϊ��";
				else
				{
					CardRemain[i]=CardRemain[i]+Money;
					cout<<"���׳ɹ�����ǰ��"<<CardRemain[i]<<endl;
				}
				system("pause");
				system("cls");
				break;
			case 4://�޸�����
				cout<<"������ԭ���룺";
				cin>>Password;
				if(Password==CardPassword[i])
				{
					cout<<"�����������룺";
					cin>>Password;
					cout<<"���ٴ��������룺";
					cin>>PasswordConfirm;
					if(Password==PasswordConfirm)
					{
						CardPassword[i]=Password;
						cout<<"\n�����޸ĳɹ���";
					}
					else
						cout<<"������������벻һ���������޸�ʧ��\n";
				}
				else
					cout<<"�������";
				system("pause");
				system("cls");
				break;
			case 9:
				cout<<"���ѳɹ��˳�\n";
				system("pause");
				system("cls");
				Status=0;
				goto Start;//ʹ��goto��õ��ȽϺõ�Ч��
			case 0://�˳�
				system("cls");
				system("color f1");
				cout<<"��ӭ���Ĺ��٣�\n�뿪ʱ�벻Ҫ���ǽ���ȡ����\n";
				system("pause");
				exit(0);
			default://������������
				cout<<"�����������������\n";
				system("pause");
				system("cls");
				break;
	}
	}
	return 0;
}