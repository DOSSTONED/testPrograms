//作业：虚拟取款机
//输入时若为非数值则会导致死循环！！
#include <iostream>
using namespace std;
int main()
{
	const int CardAccount[3]={123,456,789};//设置卡号
	int CardPassword[3]={123,456,789},Retry=3,Status=0,Choice,i;//设置密码，重试次数，登录状态，选择，第几张卡
	double CardRemain[3]={0.0,0.0,0.0},Money=0;//卡上余额，存或取的钱数
	int Account,Password,PasswordConfirm;//当前卡号与密码，确认密码
Start:system("color 71");//调用命令改变颜色
	  system("mode con cols=55 lines=15");//设置窗口大小
	cout<<"\t\t欢迎来到虚拟ATM\n";
	while(Status==0)
	{
		cout<<"请输入帐号：";
		cin>>Account;
		for(i=0;i<3;i++)//判断是第几张卡
			if(Account==CardAccount[i])
				break;//此行删除之后会造成输入错误时出现3遍错误
		if(Account==CardAccount[i])
		{
			while(Retry>0)
			{
				cout<<"请输入密码：";
				cin>>Password;
					if(Account==CardAccount[i]&&Password==CardPassword[i])//卡号密码对应
					{
						Status=1;
						break;
					}
					else//密码错误处理
					{
						Retry=Retry-1;
						if(Retry>0)
							cout<<"密码错误，请重试，您还有"<<Retry<<"次重试机会\n";
						else
						{
							cout<<"您已经超出了重试次数，已吞卡\n";
							system("color ac");
							system("pause");
							exit(0);
						}
					}
			}
		}
		else
			cout<<"无此账号，请重新输入\n";
	}
	system("cls");
	system("color 1e");
	while(1)
	{
		cout<<"当前登录账号为："<<Account<<endl;
		cout<<"　　　　　　　　╭─────────╮　　　　　　　　\n";
		cout<<"╭───────┤欢迎来到虚拟取款机├───────╮\n";
		cout<<"│　　　　　　　╰─────────╯　　　　　　　│\n";
		cout<<"│本取款机目前提供以下功能：　　　　　　　　　　　　│\n"
			<<"│1. 查询余额　　　　　　　　　　　　　　2. 提取现金│\n"
			<<"│3. 存款　　　　　　　　　　　　　　　　4. 修改密码│\n"
			<<"│9. 退出本卡　　　　　　　　　　　　　　0. 离开银行│\n";
		cout<<"╰─────────────────────────╯\n";
		cout<<"请输入相应数字进行操作：";
		cin>>Choice;
		switch (Choice)
		{
			case 1://查询余额
				cout<<"当前余额："<<CardRemain[i]<<endl;
				system("pause");
				system("cls");
				break;
			case 2://提取现金
				cout<<"请输入您要提取的现金数：";
				cin>>Money;
				if((CardRemain[i]-Money)<0)
				{
					cout<<"\n交易失败：余额不足，无法提取";
				}
				else
				{
					CardRemain[i]=CardRemain[i]-Money;
					cout<<"交易成功，当前余额："<<CardRemain[i]<<endl;
				}
				system("pause");
				system("cls");
				break;
			case 3://存款
				cout<<"请输入您要存入的现金数：";
				cin>>Money;
				if(Money<=0)
					cout<<"\n交易失败：存入现金数不能为负";
				else
				{
					CardRemain[i]=CardRemain[i]+Money;
					cout<<"交易成功，当前余额："<<CardRemain[i]<<endl;
				}
				system("pause");
				system("cls");
				break;
			case 4://修改密码
				cout<<"请输入原密码：";
				cin>>Password;
				if(Password==CardPassword[i])
				{
					cout<<"请输入新密码：";
					cin>>Password;
					cout<<"请再次输入密码：";
					cin>>PasswordConfirm;
					if(Password==PasswordConfirm)
					{
						CardPassword[i]=Password;
						cout<<"\n密码修改成功！";
					}
					else
						cout<<"两次输入的密码不一样，密码修改失败\n";
				}
				else
					cout<<"密码错误！";
				system("pause");
				system("cls");
				break;
			case 9:
				cout<<"卡已成功退出\n";
				system("pause");
				system("cls");
				Status=0;
				goto Start;//使用goto会得到比较好的效果
			case 0://退出
				system("cls");
				system("color f1");
				cout<<"欢迎您的光临！\n离开时请不要忘记将卡取出！\n";
				system("pause");
				exit(0);
			default://输入其他数字
				cout<<"输入错误，请重新输入\n";
				system("pause");
				system("cls");
				break;
	}
	}
	return 0;
}