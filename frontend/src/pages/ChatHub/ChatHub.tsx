import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import './ChatHub.css';

const ChatHub: React.FC = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [username, setUsername] = useState('');
  const [message, setMessage] = useState('');
  const [messages, setMessages] = useState<string[]>([]);
  const [isConnected, setIsConnected] = useState(false);

  // Create and start the connection
  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chatHub') // 🔁 Change if your backend URL/port is different
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  // Start connection and listen for messages
  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log('✅ SignalR connected');
          setIsConnected(true);

          connection.on('ReceiveMessage', (user: string, message: string) => {
            console.log('📩 Message received:', user, message);
            setMessages(prev => [...prev, `${user}: ${message}`]);
          });
        })
        .catch(error => {
          console.error('❌ SignalR connection failed:', error);
        });
    }
  }, [connection]);

  const sendMessage = async () => {
    if (connection?.state === signalR.HubConnectionState.Connected && username && message) {
      try {
        await connection.invoke('SendMessage', username, message);
        setMessage('');
      } catch (error) {
        console.error('❌ Send failed:', error);
      }
    } else {
      console.warn('⚠️ Cannot send message — connection not ready or missing input.');
    }
  };

  return (
    <div className="chat-container">
      <h2>Real-Time Chat</h2>

      <input
        className="chat-input"
        placeholder="Your name"
        value={username}
        onChange={e => setUsername(e.target.value)}
      />

      <input
        className="chat-input"
        placeholder="Type a message..."
        value={message}
        onChange={e => setMessage(e.target.value)}
        onKeyDown={e => e.key === 'Enter' && sendMessage()}
      />

      <button
        className="chat-send-button"
        onClick={sendMessage}
        disabled={!isConnected}
      >
        Send
      </button>

      <div className="chat-messages">
        {messages.map((msg, idx) => (
          <div key={idx} className="chat-message">
            {msg}
          </div>
        ))}
      </div>
    </div>
  );
};

export default ChatHub;