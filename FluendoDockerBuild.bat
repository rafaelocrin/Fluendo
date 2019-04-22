@ECHO OFF

@echo **********************
@echo Bulding Authentication
@echo **********************

docker build --no-cache -f Dockerfile_Authentication .


@echo **********************
@echo Bulding Core
@echo **********************

docker build --no-cache -f Dockerfile_Core .


@echo **********************
@echo Bulding Stats
@echo **********************


docker build --no-cache -f Dockerfile_Stats .