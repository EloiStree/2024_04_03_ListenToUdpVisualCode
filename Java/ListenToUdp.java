import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.nio.charset.StandardCharsets;

public class ListenToUdp {
    public static void main(String[] args) {
        int port1=2512, port2=2514;
      
        try {
            port1 = Integer.parseInt(args[0]);
        } catch (Exception  e) {} 
         try {
            port2 = Integer.parseInt(args[1]);
        } catch (Exception  e) {}

        final int finalPort1 = port1;
        Thread thread1 = new Thread(() -> listenToUdp(finalPort1, "UTF-8"));
        final int finalPort2 = port2;
        Thread thread2 = new Thread(() -> listenToUdp(finalPort2, "Unicode"));

        thread1.start();
        thread2.start();
    }

    private static void listenToUdp(int port, String charset) {
        
         System.out.println("Listen to  " + port + ": " + charset);
        try (DatagramSocket socket = new DatagramSocket(port)) {
            byte[] buffer = new byte[1024];
            DatagramPacket packet = new DatagramPacket(buffer, buffer.length);

            while (true) {
                socket.receive(packet);
                String message = new String(packet.getData(), 0, packet.getLength(), charset);
                System.out.println("Received message on port " + port + ": " + message);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}