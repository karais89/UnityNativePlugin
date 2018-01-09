//
//  UnityNativePlugin.m
//  Unity-iPhone
//
//  Created by JH on 2018. 1. 9..
//

#import <Foundation/Foundation.h>

// UnityNativePlugin 클래스 정의
@interface UnityNativePlugin : NSObject {
    NSString* unityGameObjName;       // 콜백을 송신할 곳을 저장
    BOOL isDebug;
    NSString* debugTag;
}

+ (UnityNativePlugin*) sharedInstance; // 싱글톤 인스턴스
@end

// UnityNativePlugin 클래스 구현
@implementation UnityNativePlugin

// 싱글톤 인스턴스를 초기화하고 가져온다
static UnityNativePlugin* sharedInstance = nil;
+ (id)sharedInstance {
    @synchronized(self) {
        if (sharedInstance == nil) {
            sharedInstance = [[self alloc] init];
        }
    }
    return sharedInstance;
}

// 네이티브 플러그인 사용시 반드시 처음에 먼저 호출해 줘야 한다.
- (void)init:(NSString*)unityGameObjectName {
    self->unityGameObjName = unityGameObjectName;
}

// 디버그 설정시 모든 api 호출시 호출 메서드가 출력 된다.
- (void)setIsDebug:(BOOL)isDebug {
    self->isDebug = isDebug;
}

- (void)setDebugTag:(NSString*)debugTag {
    self->debugTag = debugTag;
}

- (void)testIOSFunc:(NSString*)strFromUnity {
    
}

- (void)sendMessage:(NSString*)methodName message:(NSString*)msg {
    
    // UnitySendMessage 메서드를 사용해 유니티 쪽 메소드를 호출한다
    UnitySendMessage([unityGameObjName cStringUsingEncoding:NSUTF8StringEncoding],
                     [methodName cStringUsingEncoding:NSUTF8StringEncoding],
                     [msg cStringUsingEncoding:NSUTF8StringEncoding]);
}

// 네이티브 플러그인 사용시 반드시 처음에 먼저 호출해 줘야 한다.
- (BOOL)clipBoardCopy:(NSString*)msg {
    [UIPasteboard generalPasteboard].string = msg;
    return YES;
}

@end

// c++ 컴파일 시에 발생하는 네임 맹글링을 피하기 위해 c 링케이지에서 선언한다.
extern "C" {
    void kara_UnityNativePlugin_init(const char* unityGameObjectName) {
        UnityNativePlugin* instance = [UnityNativePlugin sharedInstance];
        @synchronized(instance) {
            [instance init: [NSString stringWithUTF8String:unityGameObjectName]];
        }
    }
    BOOL kara_UnityNativePlugin_clipBoardCopy(const char* msg) {
        UnityNativePlugin* instance = [UnityNativePlugin sharedInstance];
        @synchronized(instance) {
            return [instance clipBoardCopy: [NSString stringWithUTF8String:msg]];
        }
    }
}
