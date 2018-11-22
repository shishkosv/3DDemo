#pragma once

namespace Oiler
{
	namespace Algo
	{
		class WeatherService; // This allows us to mention it in this header file
					 // without actually including the native version of Logic.h

		namespace CLI
		{
			// Next is the managed wrapper of Logic:
			public ref class WeatherService
			{
			public:
				// Managed wrappers are generally less concerned 
				// with copy constructors and operators, since .NET will
				// not call them most of the time.
				// The methods that do actually matter are:
				// The constructor, the "destructor" and the finalizer
				WeatherService();
				~WeatherService();
				!WeatherService();

				int Get();

				void Destroy(); // Helper function
			private:
				// Pointer to our implementation
				Oiler::Algo::WeatherService* _impl;
			};
		}
	}
}