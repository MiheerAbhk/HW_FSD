import React, { useState } from 'react';
import { Search, MapPin, Calendar, Users, ArrowRight } from 'lucide-react';
import { Link } from 'react-router-dom';
import api from '../../api/config';

const FlightSearch = () => {
    const [searchParams, setSearchParams] = useState({
        origin: '',
        destination: '',
        departureDate: '',
        passengers: 1
    });
    const [flights, setFlights] = useState([]);
    const [loading, setLoading] = useState(false);
    const [searched, setSearched] = useState(false);

    const handleSearch = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSearched(true);

        try {
            // Mock flight search results
            await new Promise(resolve => setTimeout(resolve, 1000));
            setFlights([
                {
                    id: 1,
                    flightNumber: 'SF001',
                    airline: 'Simply Fly Airlines',
                    origin: 'New York',
                    destination: 'Los Angeles',
                    departureTime: '14:30',
                    arrivalTime: '17:45',
                    duration: '5h 15m',
                    price: 299.99,
                    availableSeats: 45
                },
                {
                    id: 2,
                    flightNumber: 'SF002',
                    airline: 'Simply Fly Airlines',
                    origin: 'New York',
                    destination: 'Los Angeles',
                    departureTime: '19:15',
                    arrivalTime: '22:30',
                    duration: '5h 15m',
                    price: 279.99,
                    availableSeats: 23
                },
                {
                    id: 3,
                    flightNumber: 'SF003',
                    airline: 'Simply Fly Airlines',
                    origin: 'New York',
                    destination: 'Los Angeles',
                    departureTime: '22:45',
                    arrivalTime: '01:30+1',
                    duration: '5h 45m',
                    price: 259.99,
                    availableSeats: 67
                }
            ]);
        } catch (error) {
            console.error('Error searching flights:', error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-7xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">Search Flights</h1>
                    <p className="mt-2 text-gray-600">Find the perfect flight for your journey</p>
                </div>

                <div className="bg-white rounded-lg shadow-md p-6 mb-8">
                    <form onSubmit={handleSearch} className="space-y-6">
                        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-2">
                                    <div className="flex items-center space-x-2">
                                        <MapPin className="h-4 w-4" />
                                        <span>From</span>
                                    </div>
                                </label>
                                <input
                                    type="text"
                                    required
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                    placeholder="Origin city"
                                    value={searchParams.origin}
                                    onChange={(e) => setSearchParams({ ...searchParams, origin: e.target.value })}
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-2">
                                    <div className="flex items-center space-x-2">
                                        <MapPin className="h-4 w-4" />
                                        <span>To</span>
                                    </div>
                                </label>
                                <input
                                    type="text"
                                    required
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                    placeholder="Destination city"
                                    value={searchParams.destination}
                                    onChange={(e) => setSearchParams({ ...searchParams, destination: e.target.value })}
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-2">
                                    <div className="flex items-center space-x-2">
                                        <Calendar className="h-4 w-4" />
                                        <span>Departure</span>
                                    </div>
                                </label>
                                <input
                                    type="date"
                                    required
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                    value={searchParams.departureDate}
                                    onChange={(e) => setSearchParams({ ...searchParams, departureDate: e.target.value })}
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-2">
                                    <div className="flex items-center space-x-2">
                                        <Users className="h-4 w-4" />
                                        <span>Passengers</span>
                                    </div>
                                </label>
                                <select
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                    value={searchParams.passengers}
                                    onChange={(e) => setSearchParams({ ...searchParams, passengers: parseInt(e.target.value) })}
                                >
                                    {[1, 2, 3, 4, 5, 6].map(num => (
                                        <option key={num} value={num}>{num} Passenger{num > 1 ? 's' : ''}</option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="flex justify-center">
                            <button
                                type="submit"
                                disabled={loading}
                                className="flex items-center space-x-2 bg-purple-600 hover:bg-purple-700 text-white px-8 py-3 rounded-md transition-colors disabled:opacity-50"
                            >
                                <Search className="h-5 w-5" />
                                <span>{loading ? 'Searching...' : 'Search Flights'}</span>
                            </button>
                        </div>
                    </form>
                </div>

                {/* Search Results */}
                {loading && (
                    <div className="bg-white rounded-lg shadow-md p-8 text-center">
                        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-purple-600 mx-auto mb-4"></div>
                        <p className="text-gray-600">Searching for flights...</p>
                    </div>
                )}

                {searched && !loading && flights.length === 0 && (
                    <div className="bg-white rounded-lg shadow-md p-8 text-center">
                        <p className="text-gray-600">No flights found for your search criteria.</p>
                    </div>
                )}

                {flights.length > 0 && (
                    <div className="space-y-4">
                        <h2 className="text-xl font-semibold text-gray-900">
                            Available Flights ({flights.length})
                        </h2>
                        {flights.map((flight) => (
                            <div key={flight.id} className="bg-white rounded-lg shadow-md p-6">
                                <div className="flex flex-col lg:flex-row lg:items-center lg:justify-between">
                                    <div className="flex-1">
                                        <div className="flex items-center space-x-4 mb-4">
                                            <div className="text-center">
                                                <p className="text-2xl font-bold text-gray-900">{flight.departureTime}</p>
                                                <p className="text-sm text-gray-500">{flight.origin}</p>
                                            </div>
                                            <div className="flex-1 text-center">
                                                <div className="flex items-center justify-center space-x-2">
                                                    <div className="h-px bg-gray-300 flex-1"></div>
                                                    <ArrowRight className="h-5 w-5 text-gray-400" />
                                                    <div className="h-px bg-gray-300 flex-1"></div>
                                                </div>
                                                <p className="text-sm text-gray-500 mt-1">{flight.duration}</p>
                                            </div>
                                            <div className="text-center">
                                                <p className="text-2xl font-bold text-gray-900">{flight.arrivalTime}</p>
                                                <p className="text-sm text-gray-500">{flight.destination}</p>
                                            </div>
                                        </div>
                                        <div className="flex items-center justify-between text-sm text-gray-600">
                                            <span>{flight.flightNumber} • {flight.airline}</span>
                                            <span>{flight.availableSeats} seats available</span>
                                        </div>
                                    </div>
                                    <div className="lg:ml-8 mt-4 lg:mt-0 text-center lg:text-right">
                                        <p className="text-2xl font-bold text-gray-900 mb-2">
                                            ${flight.price}
                                        </p>
                                        <Link
                                            to={`/passenger/book/${flight.id}`}
                                            className="inline-block bg-purple-600 hover:bg-purple-700 text-white px-6 py-2 rounded-md transition-colors"
                                        >
                                            Select Flight
                                        </Link>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

export default FlightSearch;