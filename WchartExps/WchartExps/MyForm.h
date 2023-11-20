/*
* Пример, демонстрирующий конфликт типов данных wchar_t и char
* при реализации чата с автопоиском сервера
* на основе примеров по TCP и UDP
*/
#pragma once
#include <stdio.h>
#include <string.h>

namespace WchartExps {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	wchar_t c[255] = L"Hello";

	char* network_space;

	void my_send(char* v) {
		//strcpy(v, "Hello");
		
		network_space = v;
		
	}

	void my_recv(char* v) {
		v = network_space;
	}

	

	/// <summary>
	/// Сводка для MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ button1;

	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Button^ button2;
	private: System::Windows::Forms::Label^ label2;
	protected:

	private:
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(25, 31);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(145, 49);
			this->button1->TabIndex = 0;
			this->button1->Text = L"variable sample";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(217, 47);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(46, 17);
			this->label1->TabIndex = 2;
			this->label1->Text = L"label1";
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(25, 108);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(145, 50);
			this->button2->TabIndex = 3;
			this->button2->Text = L"function sample";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &MyForm::button2_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(217, 108);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(46, 17);
			this->label2->TabIndex = 4;
			this->label2->Text = L"label2";
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(8, 16);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(432, 253);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->button1);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {
		wchar_t bufw_recv[501], bufw_send[501]=L"That is wchar_t string";
		char* tmp, buf_send[501]="That is classic C string";

		//char *bufc="Hello classic Char!";
		//char *tmp 
		//func((char *)bufw);

		
		tmp = (char*)bufw_recv;
		//memcpy(tmp, bufw_send, sizeof(bufw_send)); //Отправляем строку wchar_t 
		strcpy(tmp,buf_send); //Отправляем строку char
		//
		// 
		//tmp = (char*)bufw2;
		
		//((char *)bufw) = tmp;//Ошибка компиляции

		//my_send((char *)bufw2);
		//my_recv((char *)bufw);

		//label1->Text = gcnew String(bufw_recv);//Принимаем строку wchar_t
		label1->Text = gcnew String((char *)bufw_recv);//Принимаем строку char
		printf("[%s]\n",bufw_recv);

		/*char test[20];
		printf("size is [%d]\n",sizeof(test));*/

	}

		   
	private: System::Void button2_Click(System::Object^ sender, System::EventArgs^ e) {
		char buf_sender[255] = "Standard C string";
		char *buf_receiver;

		printf("buf_sender = [%x] buf_receiver = [%x] network_space = [%x]\n",buf_sender, buf_receiver, network_space);
		my_send(buf_sender);
		printf("buf_sender=[%x] buf_receiver=[%x] network_space=[%x]\n", buf_sender, buf_receiver, network_space);
		my_recv(buf_receiver);
		printf("buf_sender=[%x] buf_receiver=[%x] network_space=[%x]\n", buf_sender, buf_receiver, network_space);

		label2->Text = gcnew String(buf_receiver);
		printf("Received: [%s]\n",buf_receiver);

	}
};

	
}
