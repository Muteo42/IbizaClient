<?php
	if(!isset($_GET['key'])) exit;
	if(!defined("SYSTEM_2")) define("SYSTEM_2", true);
	include('hash.php');
	$t_securitykey = SifreCoz($_GET['key']);
	if($t_securitykey != "R3V3R531BJQYHFBBAW" && $t_securitykey != "C0D3MB519PR0T3C711" && $t_securitykey != "4DV4NC3DPR0T3C7QSZ" && $t_securitykey != "UNR3L14BL3H33XDRWZ") exit;
	$cleoList = "anticrasher037.cs|18416|3f628963|";
	echo Sifrele($cleoList);
?>