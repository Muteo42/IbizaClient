<?php
	if(!isset($_GET['key'])) exit;
	if(!defined("SYSTEM_2")) define("SYSTEM_2", true);
	include('hash.php');
	$t_Array = explode("|", SifreCoz($_GET['key']));
	if(count($t_Array) != 8)
	{
		echo Sifrele("NULL");
		exit;
	}
	$t_securitykey = $t_Array[0];
	if($t_securitykey != "R3V3R531BJQYHFBBAW" && $t_securitykey != "C0D3MB519PR0T3C711" && $t_securitykey != "4DV4NC3DPR0T3C7QSZ" && $t_securitykey != "UNR3L14BL3H33XDRWZ") exit;
	if(strlen($t_Array[1]) < 3 || strlen($t_Array[1]) > 24 || $t_Array[2] != "0.0.0.1" || strlen($t_Array[3]) < 6 || strlen($t_Array[4]) < 6  || strlen($t_Array[5]) < 6 || strlen($t_Array[6]) < 1 || $t_Array[7] != "MUTEO42")
	{
		echo Sifrele("NULL");
		exit;
	}
	$t_nick = substr(str_shuffle('0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ'), 1, 7);
	$t_sqlnick = "Ibiza_";
	$t_sqlnick .= $t_nick;
	if (!empty($_SERVER['HTTP_CLIENT_IP'])) $_SERVER['HTTP_CF_CONNECTING_IP']=$_SERVER['HTTP_CLIENT_IP'];
	elseif (!empty($_SERVER['HTTP_X_FORWARDED_FOR'])) $_SERVER['HTTP_CF_CONNECTING_IP']=$_SERVER['HTTP_X_FORWARDED_FOR'];
	else $_SERVER['HTTP_CF_CONNECTING_IP']=$_SERVER['REMOTE_ADDR'];
	require_once("/var/www/south-central.net/public_html/services/iclient/connect.php");
	$sql = $iconn -> prepare('INSERT INTO client (nickname, tempname, CPUID, MAC, MSERIAL, RAM, ip) VALUES(:nick, :tempnick, :cpu, :macid, :serial, :raminfo, :ip)');
	$sql -> execute([
		'nick' => $t_Array[1],
		'tempnick' => $t_sqlnick,
		'cpu' => $t_Array[3],
		'macid' => $t_Array[4],
		'serial' => $t_Array[5],
		'raminfo' => $t_Array[6],
		'ip' => $_SERVER['HTTP_CF_CONNECTING_IP']
	]);
	if($sql)
	{
		echo Sifrele($t_nick);
		exit;
	}
	echo Sifrele("NULL");
	exit;
?>