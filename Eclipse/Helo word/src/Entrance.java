//import Test1.ClassPub;



public class Entrance {
	public static void main(String[] args)
	{
		Test1 t = new Test1();
		t.printCur();
		Test1.ClassPub c = new Test1.ClassPub(), ss = new Test1.ClassPub();
		c.cp_a = 0x2222;
		c.printCP();
		ss.printCP();
		ss = c;
		ss.printCP();
		//Object aaa = t.new ClassPub();
	}

}

