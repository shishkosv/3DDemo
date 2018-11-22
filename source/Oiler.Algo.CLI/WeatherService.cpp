
#include "WeatherService.h"
#include "..\Oiler.Algo.Cpp\WeatherService.h"

using namespace std;

//Oiler::Algo::CLI::WeatherService : _impl(new Cpp::WeatherService())
//	// Allocate some memory for the native implementation
//{
//}

int Oiler::Algo::CLI::WeatherService::Get()
{
	return _impl->Get(); // Call native Get
}

void Oiler::Algo::CLI::WeatherService::Destroy()
{
	if (_impl != nullptr)
	{
		delete _impl;
		_impl = nullptr;
	}
}

Oiler::Algo::CLI::WeatherService::~WeatherService()
{
	// C++ CLI compiler will automaticly make all ref classes implement IDisposable.
	// The default implementation will invoke this method + call GC.SuspendFinalize.
	Destroy(); // Clean-up any native resources 
}

Oiler::Algo::CLI::WeatherService::!WeatherService()
{
	// This is the finalizer
	// It's essentially a fail-safe, and will get called
	// in case Logic was not used inside a using block.
	Destroy(); // Clean-up any native resources 
}