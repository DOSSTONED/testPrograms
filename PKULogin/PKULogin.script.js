if (top.bottomFrame) {
	if (top.bottomFrame != self) {
		alert("系统超时或已经退出登录，请重新登录\nSession Timeout,pls login again");
		top.location.replace("https://its.pku.edu.cn/")
	}
}
//function coverErrors(){return true;}
//window.onerror = coverErrors;
function doLogin(evt) {
	evt = (evt) ? evt : ((window.event) ? window.event : "");
	var key = evt.keyCode ? evt.keyCode : evt.which;
	if (evt.ctrlKey && (key == 13 || key == 10)) {	//Ctrl+Enter, that is login mail only.
		SendEntryData(4);
		return;
	}
	if (key == 13) {	//Enter, login action. This one is tricky since you might use enter to select in the listbox entry.
		SendEntryData(1);
	}
}
function PwdInput(pwd_item) {
	var pwd = document.getElementById("password");
	pwd_item.style.display = 'none';
	pwd.value = '';
	pwd.style.display = '';
	pwd.focus();
}
function PwdInput2(p_item) {
	var pdt = document.getElementById("pwd_t");
	p_item.style.borderColor = '#cecfce';
	if (p_item.value == '') {
		p_item.style.display = 'none';
		pdt.style.display = '';
	}
}
function SendEntryData(iOp) {
	if (iOp == 2) {
		window.location.replace("https://its.pku.edu.cn:8443/cas/login");
		return;
	}
	var _fr = document.getElementById("lif");
	var _Un = document.getElementById("username1").value.replace(/(^\s*)|(\s*$)/g, "");
	var _Pd = document.getElementById("password").value;
	if (_Un.length == 0 || _Un == "校园网账号／北大邮箱") {
		alert(" 请输入校园网账号或北大邮箱   \n User ID & Mail Address Needed   ");
		document.getElementById("username1").focus();
		return;
	}
	if (_Un.indexOf("*") > -1 || _Un.indexOf("?") > -1) {
		alert(" 输入错误，请重新输入   \n UserID not known, pls input again");
		document.getElementById("username1").focus();
		document.getElementById("username1").select();
		return;
	}
	if (_Pd.length == 0) {
		alert(" 密码不能为空   \n Password Needed   ");
		document.getElementById("password").focus();
		return;
	}
	if (_Un != "" && _Pd != "") {
		document.getElementById("username").value = _Un + unescape("%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C") + _Pd;
		var _rg = document.getElementsByName("fwrd");
		if (iOp == 1) {	// Login
			for (j = 0; j < _rg.length; j++) {
				if (_rg[j].checked && _rg[j].value == "fee") {	// Global
					document.getElementById("username").value = document.getElementById("username").value + unescape("%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C") + "11";
					_fr.submit();
					return;
				}
				if (_rg[j].checked && _rg[j].value == "free") {	// Free
					document.getElementById("username").value = document.getElementById("username").value + unescape("%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C") + "12";
					_fr.submit();
					return;
				}
				if (_rg[j].checked && _rg[j].value == "noopen") {
					document.getElementById("username").value = document.getElementById("username").value + unescape("%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C") + "15";
					_fr.submit();
					return;
				}
			}
		} else {
			if (iOp == 3) {	// Disconnect all
				document.getElementById("username").value = document.getElementById("username").value + unescape("%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C") + "13";
				_fr.submit();
				return;
			}
			if (iOp == 4) {	//Mail login
				document.getElementById("username").value = document.getElementById("username").value + unescape("%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C") + "16";
				_fr.submit();
				return;
			}
		}
	} else {
		alert(" 用户名、密码不能为空   \n UserID & Password Needed ");
		document.getElementById("username1").focus();
		return;
	}
}
function setfocus() {
	var un1 = document.getElementById("username1");
	un1.focus();
	document.getElementById("i1").checked = true;
}
function ccv() {
	document.getElementById("cvp").src = "/image.jsp?" + Math.random();
	document.getElementById("cv").value = "";
}