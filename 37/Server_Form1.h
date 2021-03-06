#pragma once

#include <winsock.h>
#include <stdio.h>

#define	WSA_NETEVENT		(WM_USER+2)
#define	WSA_NETACCEPT		(WM_USER+3)

#define SERVER_PORT     3021

SOCKET         TCPSocket, TmpSocket;
sockaddr_in	   CallAddress;
sockaddr_in	   OurAddress;
int			   f=0;


namespace Server {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
    using namespace System::Security::Permissions;

	/// <summary>
	/// Summary for Form1
	///
	/// WARNING: If you change the name of this class, you will need to change the
	///          'Resource File Name' property for the managed resource compiler tool
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::ListBox^  listBox1;
	private: System::Windows::Forms::ListBox^  listBox2;
	private: System::Windows::Forms::TextBox^  textBox1;
	private: System::Windows::Forms::Button^  button1;

	protected: 

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->listBox1 = (gcnew System::Windows::Forms::ListBox());
			this->listBox2 = (gcnew System::Windows::Forms::ListBox());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// listBox1
			// 
			this->listBox1->FormattingEnabled = true;
			this->listBox1->ItemHeight = 16;
			this->listBox1->Location = System::Drawing::Point(1, 1);
			this->listBox1->Name = L"listBox1";
			this->listBox1->Size = System::Drawing::Size(262, 276);
			this->listBox1->TabIndex = 0;
			// 
			// listBox2
			// 
			this->listBox2->FormattingEnabled = true;
			this->listBox2->ItemHeight = 16;
			this->listBox2->Location = System::Drawing::Point(269, 1);
			this->listBox2->Name = L"listBox2";
			this->listBox2->Size = System::Drawing::Size(257, 212);
			this->listBox2->TabIndex = 1;
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(269, 219);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(254, 22);
			this->textBox1->TabIndex = 2;
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(352, 247);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(97, 30);
			this->button1->TabIndex = 3;
			this->button1->Text = L"Отправить";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &Form1::button1_Click);
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(8, 16);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(538, 289);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->listBox2);
			this->Controls->Add(this->listBox1);
			this->Name = L"Form1";
			this->Text = L"Server";
			this->FormClosed += gcnew System::Windows::Forms::FormClosedEventHandler(this, &Form1::Form1_FormClosed);
			this->Activated += gcnew System::EventHandler(this, &Form1::Form1_Activated);
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

// -----------------------------------------------------------------------------------------------------------------
	private: System::Void Form1_Activated(System::Object^  sender, System::EventArgs^  e) {
			 WSADATA	    WSAData;
			 int		    rc;
			 char			Name[101], *IpAddr, Buf[1000];
			 PHOSTENT       phe;
			 
			 if (f==1) return;
			 f = 1;
			 listBox1->Items->Add("WSAStartup");
			 rc = WSAStartup(MAKEWORD(2,0), &WSAData);
			 if (rc != 0) {
				listBox1->Items->Add("Ошибка инициализации WSAStartup");
				return;
			} // if

			TCPSocket = socket(AF_INET, SOCK_STREAM, 0);
			if (TCPSocket == INVALID_SOCKET) {
				listBox1->Items->Add("Протокол TCP установлен.");
			} // if

			memset(&OurAddress, 0, sizeof(OurAddress));
			OurAddress.sin_family = AF_INET;
			OurAddress.sin_port = htons(SERVER_PORT);

		    rc =bind(TCPSocket, (LPSOCKADDR)&OurAddress, sizeof(sockaddr_in));
			if (rc == SOCKET_ERROR) {
				listBox1->Items->Add("Адресная ошибка");
				return;
			} // if
			
			rc = WSAAsyncSelect(TCPSocket, (HWND)(this->Handle.ToInt32()), WSA_NETACCEPT, FD_ACCEPT);
			if (rc != 0) {
				listBox1->Items->Add("Ошибка WSAAsyncSelect");
				return;
			} // if

			rc = listen(TCPSocket, 1);
			if (rc == SOCKET_ERROR) {
				listBox1->Items->Add("Ошибка listen");
				return;
			} // if

			gethostname(Name, 101);
			strcpy(Buf, "Имя компьютера ");
			strcat(Buf, Name);
			String ^ s= gcnew String(Buf);
			listBox1->Items->Add(s);

			phe = gethostbyname(Name);
			if (phe != NULL) {
				memcpy((void *)&(OurAddress.sin_addr), phe->h_addr, phe->h_length);
				IpAddr = inet_ntoa(OurAddress.sin_addr);
				strcpy(Buf, "IP-Адрес ");
				strcat(Buf, IpAddr);
				String ^ s2= gcnew String(Buf);
				listBox1->Items->Add(s2);
			} // if

			listBox1->Items->Add(L"Сервер запущен");
		 } // Form1_Activated
// -----------------------------------------------------------------------------------------------------------------
    protected:
    virtual void WndProc (Message% m) override
	{
		int      rc, l=sizeof(CallAddress);
        char	Buf[10001];
		wchar_t *Buf2, *Begin, *End;

		if (m.Msg == WSA_NETACCEPT) {
			if (m.LParam.ToInt32() == FD_ACCEPT) {
				TmpSocket = accept((SOCKET)m.WParam.ToInt32(), (PSOCKADDR)&CallAddress, (int *)&l);
				if (TmpSocket == INVALID_SOCKET) {
          			rc = WSAGetLastError();
          			listBox1->Items->Add(String::Format( "Ошибка accept {0}", rc ));
		          	return;
				} // if
				rc = WSAAsyncSelect(TmpSocket, (HWND)(this->Handle.ToInt32()), WSA_NETEVENT, FD_READ|FD_CLOSE);
				if (rc != 0) {
					listBox1->Items->Add("Ошибка WSAAsyncSelect");
					return;
				} // if
				listBox1->Items->Add("Канал создан");
			} // if
		} // if
		if (m.Msg == WSA_NETEVENT) {
			if (m.LParam.ToInt32() == FD_READ) {
				rc = recv((SOCKET)m.WParam.ToInt32(), (char *)Buf, sizeof(Buf)-1, 0);
				if (rc == SOCKET_ERROR) {
          			rc = WSAGetLastError();
          			listBox1->Items->Add(String::Format( "Ошибка recv {0}", rc ));
		          	return;
				} // if
				if (rc >= 1) {
					l = strlen(Buf);
					Buf2 = (wchar_t *)(Buf+l-1);
					Begin = wcsstr(Buf2, L"Begin=");
					if (Begin == NULL) {
						listBox1->Items->Add(L"Нет текста Begin=");
						return;
					} // if
					Begin = Begin + 6;
					End = wcsstr(Buf2, L"End");
					if (End == NULL) {
						listBox1->Items->Add(L"Нет текста End");
						return;
					} // if
					*End = '\0';
					String ^ s= gcnew String(Begin);
					listBox2->Items->Add(L"Получено " + s);					   
					
				} // if
			} else {
				listBox1->Items->Add("Канал разорван");
			} // else
		} // if
        Form::WndProc( m );
      } // WndProc
// -----------------------------------------------------------------------------------------------------------------
private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) {
		 int      rc, i, l;
		 wchar_t  Buf[1001], Buf2[1001];
				 
		 l = textBox1->Text->Length;
		 if (l == 0) return;
		 for (i=0; i < l; i++) {
			Buf[i] = textBox1->Text->default[i];
			Buf[i+1] = 0;
		 } // for

		 wcscpy(Buf2, L"Begin=");
 		 wcscat(Buf2, Buf);
		 wcscat(Buf2, L"End");
		 
		 rc = send(TmpSocket, (char *)Buf2, 2*(10+l), 0);
         if (rc == SOCKET_ERROR) {
			rc = WSAGetLastError();
			listBox1->Items->Add(String::Format( "Ошибка send {0}", rc ));
			return;
		 } // if
		 listBox1->Items->Add(textBox1->Text);		
		 closesocket(TmpSocket);
		} // button1_Click
 // -----------------------------------------------------------------------------------------------------------------
	private: System::Void Form1_FormClosed(System::Object^  sender, System::Windows::Forms::FormClosedEventArgs^  e) {
				closesocket(TCPSocket);
				WSACleanup();
			 } //Form1_FormClosed
};
}

