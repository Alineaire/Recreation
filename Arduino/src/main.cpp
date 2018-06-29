#include "FastLED.h"
#include <Arduino.h>
#include <CmdMessenger.h>

CmdMessenger cmdMessenger{ Serial };

enum Command {
    UnknownCommand,
    InvalidArgument,

    ReadyRequest,
    ReadyResponse,

    ButtonsStateResponse,

    SetButtonColorRequest,
    SetButtonColorResponse,

    TurnOffRequest,
    TurnOffResponse,
};

const uint32_t UartSpeed = 9600;

const int ButtonCount = 15;
const int LedsPerButton = 17;
CRGB leds[ButtonCount][LedsPerButton];

void OnUnknownCommand()
{
    cmdMessenger.sendCmd(Command::UnknownCommand);
}

void OnReadyRequest()
{
    cmdMessenger.sendCmd(Command::ReadyResponse, "Ready!");
}

void OnSetButtonColorRequest()
{
    auto buttonIndex = cmdMessenger.readInt16Arg();
    auto r = cmdMessenger.readInt16Arg();
    auto g = cmdMessenger.readInt16Arg();
    auto b = cmdMessenger.readInt16Arg();

    if (buttonIndex < 0 || buttonIndex >= ButtonCount
        || r < 0 || r > 255
        || g < 0 || g > 255
        || b < 0 || b > 255) {
        cmdMessenger.sendCmd(Command::InvalidArgument);
        return;
    }

    CRGB color{ r, g, b };
    for (int i = 0; i < LedsPerButton; i++) {
        leds[buttonIndex][i] = color;
    }

    cmdMessenger.sendCmd(Command::SetButtonColorResponse);
}

void OnTurnOffRequest()
{
    for (int buttonIndex = 0; buttonIndex < ButtonCount; buttonIndex++) {
        for (int i = 0; i < LedsPerButton; i++) {
            leds[buttonIndex][i] = CRGB::Black;
        }
    }

    cmdMessenger.sendCmd(Command::TurnOffResponse);
}

void setup()
{
    Serial.begin(UartSpeed);
    while (!Serial) {
        delay(10);
    }

    for (int buttonIndex = 0; buttonIndex < ButtonCount; buttonIndex++) {
        pinMode(23 + buttonIndex * 2, INPUT_PULLUP);
    }

    FastLED.addLeds<WS2812B, 22, GRB>(leds[0], LedsPerButton);
    FastLED.addLeds<WS2812B, 24, GRB>(leds[1], LedsPerButton);
    FastLED.addLeds<WS2812B, 26, GRB>(leds[2], LedsPerButton);
    FastLED.addLeds<WS2812B, 28, GRB>(leds[3], LedsPerButton);
    FastLED.addLeds<WS2812B, 30, GRB>(leds[4], LedsPerButton);
    FastLED.addLeds<WS2812B, 32, GRB>(leds[5], LedsPerButton);
    FastLED.addLeds<WS2812B, 34, GRB>(leds[6], LedsPerButton);
    FastLED.addLeds<WS2812B, 36, GRB>(leds[7], LedsPerButton);
    FastLED.addLeds<WS2812B, 38, GRB>(leds[8], LedsPerButton);
    FastLED.addLeds<WS2812B, 40, GRB>(leds[9], LedsPerButton);
    FastLED.addLeds<WS2812B, 42, GRB>(leds[10], LedsPerButton);
    FastLED.addLeds<WS2812B, 44, GRB>(leds[11], LedsPerButton);
    FastLED.addLeds<WS2812B, 46, GRB>(leds[12], LedsPerButton);
    FastLED.addLeds<WS2812B, 48, GRB>(leds[13], LedsPerButton);
    FastLED.addLeds<WS2812B, 50, GRB>(leds[14], LedsPerButton);

    cmdMessenger.printLfCr();
    cmdMessenger.attach(OnUnknownCommand);
    cmdMessenger.attach(Command::ReadyRequest, OnReadyRequest);
    cmdMessenger.attach(Command::SetButtonColorRequest, OnSetButtonColorRequest);
    cmdMessenger.attach(Command::TurnOffRequest, OnTurnOffRequest);
}

void loop()
{
    cmdMessenger.feedinSerialData();

    cmdMessenger.sendCmdStart(Command::ButtonsStateResponse);
    for (int buttonIndex = 0; buttonIndex < ButtonCount; buttonIndex++) {
        auto state = digitalRead(23 + buttonIndex * 2);
        cmdMessenger.sendCmdArg(state == LOW);
    }
    cmdMessenger.sendCmdEnd();

    FastLED.show();
}
