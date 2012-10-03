import urllib, urllib2, cookielib

username1='USERNAME_DOSSTONED'
password='PWDOSSTONED'
pwd_t='%E5%AF%86%E7%A0%81'
fwrd='free'
username='USERNAME_DOSSTONED%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7CPWDOSSTONED%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C12'

cj = cookielib.CookieJar()
opener = urllib2.build_opener(urllib2.HTTPCookieProcessor(cj))
login_data = urllib.urlencode({'username1' : username1, 'password' : password, 'pwd_t' : pwd_t, 'fwrd' : fwrd, 'username' : username})
opener.open('https://its.pku.edu.cn/cas/login', login_data)
resp = opener.open('https://its.pku.edu.cn/netportal/ipgwopen')
print resp.read()
