<?php

spl_autoload_register('AutoLoader');

function AutoLoader($className) {
    $extension = '.php';
    $fullPath = $className . $extension;

    if (is_readable($fullPath)){
        include_once $fullPath;
    }

}