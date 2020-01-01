public class X extends Thread implements Runnable{
 public void run(){
  System.out.println("this is run()");
 }
 public static void main(String args[])
	{
	Thread t=new Thread(new X());
	t.start();
	}
} 
