#pragma once

#include <windows.h>
#include <wininet.h>
#include "rapidjson/document.h"




namespace Project8 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::Text;
	using namespace rapidjson;


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
	private: System::Windows::Forms::DataGridView^ dataGridView1;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Author;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Title;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Year;
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
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->Author = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Title = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Year = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			this->SuspendLayout();
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(52, 258);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(168, 79);
			this->button1->TabIndex = 0;
			this->button1->Text = L"button1";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// dataGridView1
			// 
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {
				this->Author,
					this->Title, this->Year
			});
			this->dataGridView1->Location = System::Drawing::Point(52, 29);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->RowHeadersWidth = 51;
			this->dataGridView1->RowTemplate->Height = 24;
			this->dataGridView1->Size = System::Drawing::Size(537, 150);
			this->dataGridView1->TabIndex = 1;
			// 
			// Author
			// 
			this->Author->HeaderText = L"Автор";
			this->Author->MinimumWidth = 6;
			this->Author->Name = L"Author";
			this->Author->Width = 125;
			// 
			// Title
			// 
			this->Title->HeaderText = L"Заглавие";
			this->Title->MinimumWidth = 6;
			this->Title->Name = L"Title";
			this->Title->Width = 125;
			// 
			// Year
			// 
			this->Year->HeaderText = L"Год";
			this->Year->MinimumWidth = 6;
			this->Year->Name = L"Year";
			this->Year->Width = 125;
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(8, 16);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(640, 366);
			this->Controls->Add(this->dataGridView1);
			this->Controls->Add(this->button1);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			this->ResumeLayout(false);

		}
#pragma endregion
	private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {
		HINTERNET hInet, hSession, hRequest;
		//Инициализация
		hInet = InternetOpen(L"MyAgent",INTERNET_OPEN_TYPE_DIRECT,NULL,NULL,0);
		//Создание подключения
		hSession=InternetConnect(hInet,L"mylibrary.loc",80,L"Sartasov", L"123", INTERNET_SERVICE_HTTP, 0, 0);
		//Формирование HTTP-запроса
		hRequest = HttpOpenRequest(hSession, L"GET", L"/webapi.php", L"HTTP/1.1", L"", NULL,
			INTERNET_FLAG_EXISTING_CONNECT | INTERNET_FLAG_NO_AUTO_REDIRECT |
			INTERNET_FLAG_KEEP_CONNECTION, 0);
		//Отправка HTTP-запроса
		HttpSendRequest(hRequest, NULL, 0, NULL, 0);

		DWORD Len = 10;
		wchar_t		Buf[1001] = { 0 };
		//Получение HTTP-ответа
		//1) Определение статуса ответа. Считывание заголовка Content-length
		HttpQueryInfo(hRequest, HTTP_QUERY_CONTENT_LENGTH, Buf, &Len, NULL);
		printf("[%d]\n",_wtoi(Buf));

		Len = _wtoi(Buf);

		char http_body[1001] = { 0 };
		//2) Принятие данных из HTTP-ответа
		InternetReadFile(hRequest, http_body, Len, &Len);
		printf("[%s]\n", http_body);

		//Парсинг JSON-данных, принятых с сервера, в специальный объект
		Document d;
		d.Parse(http_body);

		
		
		String ^S = gcnew String(
			d[0].FindMember("author")->value.GetString(),
			0,
			d[0].FindMember("author")->value.GetStringLength(),
			Encoding::UTF8
		);

		String^ author;
		String^ title;
		String^ year;
		for (int i = 0;i < 3;i++) {
			printf(
				"%s\t%s\t%s\n",
				d[i].FindMember("author")->value.GetString(),
				d[i].FindMember("title")->value.GetString(),
				d[i].FindMember("year")->value.GetString()
			);

			author = gcnew String(
				d[i].FindMember("author")->value.GetString(),
				0,
				d[i].FindMember("author")->value.GetStringLength(),
				Encoding::UTF8
			);

			title = gcnew String(
				d[i].FindMember("title")->value.GetString(),
				0,
				d[i].FindMember("title")->value.GetStringLength(),
				Encoding::UTF8
			);

			year = gcnew String(
				d[i].FindMember("year")->value.GetString(),
				0,
				d[i].FindMember("year")->value.GetStringLength(),
				Encoding::UTF8
			);

			this->dataGridView1->Rows->Add(
				author,
				title,
				year
			);
			
		}
		//printf("[%s]\n", s);
		
		this->button1->Text = S;

		
	}

		   
		   
	};
}
