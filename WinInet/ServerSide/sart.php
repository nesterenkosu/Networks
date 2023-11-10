<?php
	require_once("$_SERVER[DOCUMENT_ROOT]/../includes/flight/Flight.php");
	file_put_contents("sart_works.txt","Sart Works!");
	$library=Array(
		Array(
			"author"=>"А.С.Пушкин",
			"title"=>"Метель",
			"year"=>"1969"
		),
		Array(
			"author"=>"Н.В.Гоголь",
			"title"=>"Мёртвые души",
			"year"=>"1970"
		),
		Array(
			"author"=>"Ф.М.Достоевский",
			"title"=>"Идиот",
			"year"=>"1990"
		),
	);
	
	//Flight::json($library);
	echo json_encode($library);
	//echo "Hello Evgeiny";
//phpinfo();