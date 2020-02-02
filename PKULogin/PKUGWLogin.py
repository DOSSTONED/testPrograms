#!/usr/bin/env python

import cookielib, urllib
import httplib

http = httplib.HTTPSConnection('its.pku.edu.cn')

url = 'https://its.pku.edu.cn/cas/login'   
body = {
'username1': 'USERNAME_DOSSTONED',
'password': 'PWDOSSTONED',
'pwd_t': '����',
'fwrd': 'free',
'username': 'USERNAME_DOSSTONED|;kiDrqvfi7d$v0p5Fg72Vwbv2;|PWDOSSTONED|;kiDrqvfi7d$v0p5Fg72Vwbv2;|12'}
headers = {'Content-type': 'application/x-www-form-urlencoded'}
http.request('POST', url, body=urllib.urlencode(body), headers=headers)
response = http.getresponse()
print response.status, response.reason
data = response.read()
print data
headers = {'Cookie': response['set-cookie']}

url = 'https://its.pku.edu.cn/netportal/ipgwopen'  
http.request('GET', url, headers=headers)
response = http.getresponse()
print response.status, response.reason
data = response.read()
print data