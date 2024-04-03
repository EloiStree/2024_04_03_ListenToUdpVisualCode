import socket

portToListen=2514

def listen_udp(port):
    # Create a UDP socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    # Bind the socket to a specific address and port
    sock.bind(('0.0.0.0', port))

    print(f"Listening for UDP packets on port {port}...")

    while True:
        # Receive data from the socket
        data, addr = sock.recvfrom(1024)
        decoded_data = data.decode('utf-8', 'ignore')
        decoded_data = decoded_data.replace(' ' ,' ')
        print(f"Received data from {addr}: {decoded_data}")

# Call the function to start listening on port 2514
listen_udp(portToListen)