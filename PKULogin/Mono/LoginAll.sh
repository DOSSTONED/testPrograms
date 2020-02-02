wget --post-data='username1=USERNAME_DOSSTONED&password=PWDOSSTONED&pwd_t=%E5%AF%86%E7%A0%81&fwrd=free&username=USERNAME_DOSSTONED%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7CPWDOSSTONED%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C11' --cookies=on --keep-session-cookies  --save-cookies=cookie.txt 'https://its.pku.edu.cn/cas/login' -O /dev/null

wget --referer="https://its.pku.edu.cn/cas/login" --cookies=on --keep-session-cookies --load-cookies=cookie.txt 'https://its.pku.edu.cn/netportal/ipgwopenall' -O ./OUTPUT 

cat OUTPUT | grep SCOPE
