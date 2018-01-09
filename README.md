# unity native plugin 만들기

이제는 유니티에서 기본적으로 [IAP 기능](https://docs.unity3d.com/Manual/UnityIAP.html)을 지원 하고, 푸시 기능 또한 기본적으로 [구글 Firebase](https://firebase.google.com/?hl=ko)를 사용하기 때문에, 이제는 굳이 에셋 스토어에서 제공해주는 플러그인을 구매할 필요성이 점점 사라지는 것 같다. 하지만 몇 가지 유니티에서 기본적으로 제공해주지 않는 기능들이 존재하기 때문에, 관련 플러그인을 구매하거나, 직접 만들어야 된다.

환경
- unity version 2017.3.0f3
- android stduio version 3.0.1
- mac os

목표
- 안드로이드, iOS를 지원하는 플러그인 만들기
- 안드로이드의 경우 mainActivity를 사용하지 않고 외부 클래스만을 이용해 네이티브 기능 지원 (플러그인과 충돌을 방지하기 위해)

아래 기능을 구현
- 클립보드 복사 기능


## Android Plugin 만들기

## iOS Plugin 만들기

iOS의 경우 빌드시 생성되는 엑스코드 프로젝트에서 플러그인을 구현하는 것이 편하다.

구현한 mm파일 및 header 파일을 유니티 프로젝트의 어느 곳에 집어 넣어도 상관없으며, 집어 넣으면

다시 빌드시에는 구현한 파일들도 포함되서 빌드가 된다.

## 트러블 슈팅

unity version 2017.3.0f3 버전 부터 안드로이드 기본 빌드 시스템이 gradle로 설정 되어 있다.

이대로 빌드를 하면 에러를 내뱉는다. 빌드 시스템을 internal로 변경해서 빌드 하자.

internal과 gradle 빌드 시스템의 차이는 아래와 같다

[유니티 공식 메뉴얼 - Gradle for Android](https://docs.unity3d.com/Manual/android-gradle-overview.html)