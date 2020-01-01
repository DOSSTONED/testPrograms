
public class Test1 {
	private int a = 0x10000000;
	public Test1()
	{
		System.out.println("Test1 construct");
	}
	public static class ClassPub
	{
		public ClassPub()
		{
			System.out.printf("ClassPub is constructed.\r\n");
		}
		public void printCP()
		{
			System.out.printf("cp_a = 0x%x\r\n", cp_a);
		}
		public int cp_a;// = 0x19;
	}
	
	public void printCur()
	{
		System.out.printf("a = 0x%x\r\n", a, a);
		//System.out.printf
	}

}
