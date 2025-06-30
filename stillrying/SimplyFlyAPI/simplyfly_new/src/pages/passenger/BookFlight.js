import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Plane, User, CreditCard } from 'lucide-react';
import api from '../../api/config';

const BookFlight = () => {
    const { flightId } = useParams();
    const navigate = useNavigate();
    const [flight, setFlight] = useState(null);
    const [selectedSeat, setSelectedSeat] = useState('');
    const [passengerInfo, setPassengerInfo] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phone: ''
    });
    const [loading, setLoading] = useState(true);
    const [booking, setBooking] = useState(false);

    useEffect(() => {
        fetchFlightDetails();
    }, [flightId]);

    const fetchFlightDetails = async () => {
        try {
            // Mock flight details
            setFlight({
                id: 1,
                flightNumber: 'SF001',
                airline: 'Simply Fly Airlines',
                origin: 'New York',
                destination: 'Los Angeles',
                departureTime: '14:30',
                arrivalTime: '17:45',
                date: '2024-01-25',
                price: 299.99,
                availableSeats: ['12A', '12B', '12C', '13A', '13B', '13C', '14A', '14B', '14C']
            });
        } catch (error) {
            console.error('Error fetching flight details:', error);
        } finally {
            setLoading(false);
        }
    };

    const handleBooking = async (e) => {
        e.preventDefault();
        setBooking(true);

        try {
            // Mock booking process
            await new Promise(resolve => setTimeout(resolve, 2000));

            const bookingId = Math.random().toString(36).substr(2, 9);
            navigate(`/passenger/payment/${bookingId}`);
        } catch (error) {
            console.error('Error creating booking:', error);
        } finally {
            setBooking(false);
        }
    };

    if (loading) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-purple-600"></div>
            </div>
        );
    }

    if (!flight) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <div className="text-center">
                    <p className="text-gray-600">Flight not found</p>
                </div>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-4xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">Book Your Flight</h1>
                    <p className="mt-2 text-gray-600">Complete your booking for {flight.flightNumber}</p>
                </div>

                <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                    <div className="lg:col-span-2 space-y-6">
                        {/* Flight Details */}
                        <div className="bg-white rounded-lg shadow-md p-6">
                            <h3 className="text-lg font-semibold text-gray-900 mb-4 flex items-center">
                                <Plane className="h-5 w-5 mr-2" />
                                Flight Details
                            </h3>
                            <div className="border-l-4 border-purple-500 pl-4">
                                <div className="flex items-center justify-between mb-2">
                                    <span className="font-medium">{flight.flightNumber}</span>
                                    <span className="text-sm text-gray-500">{flight.airline}</span>
                                </div>
                                <div className="flex items-center space-x-4 text-sm text-gray-600">
                                    <span>{flight.origin}</span>
                                    <span>→</span>
                                    <span>{flight.destination}</span>
                                </div>
                                <div className="flex items-center space-x-4 text-sm text-gray-600 mt-1">
                                    <span>{new Date(flight.date).toLocaleDateString()}</span>
                                    <span>{flight.departureTime} - {flight.arrivalTime}</span>
                                </div>
                            </div>
                        </div>

                        {/* Seat Selection */}
                        <div className="bg-white rounded-lg shadow-md p-6">
                            <h3 className="text-lg font-semibold text-gray-900 mb-4">Select Your Seat</h3>
                            <div className="grid grid-cols-3 gap-2 max-w-xs">
                                {flight.availableSeats.map((seat) => (
                                    <button
                                        key={seat}
                                        onClick={() => setSelectedSeat(seat)}
                                        className={`p-2 text-sm font-medium rounded border-2 transition-colors ${selectedSeat === seat
                                                ? 'border-purple-500 bg-purple-100 text-purple-700'
                                                : 'border-gray-300 hover:border-purple-300 text-gray-700'
                                            }`}
                                    >
                                        {seat}
                                    </button>
                                ))}
                            </div>
                        </div>

                        {/* Passenger Information */}
                        <div className="bg-white rounded-lg shadow-md p-6">
                            <h3 className="text-lg font-semibold text-gray-900 mb-4 flex items-center">
                                <User className="h-5 w-5 mr-2" />
                                Passenger Information
                            </h3>
                            <form onSubmit={handleBooking} className="space-y-4">
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                    <div>
                                        <label className="block text-sm font-medium text-gray-700 mb-1">
                                            First Name
                                        </label>
                                        <input
                                            type="text"
                                            required
                                            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                            value={passengerInfo.firstName}
                                            onChange={(e) => setPassengerInfo({ ...passengerInfo, firstName: e.target.value })}
                                        />
                                    </div>
                                    <div>
                                        <label className="block text-sm font-medium text-gray-700 mb-1">
                                            Last Name
                                        </label>
                                        <input
                                            type="text"
                                            required
                                            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                            value={passengerInfo.lastName}
                                            onChange={(e) => setPassengerInfo({ ...passengerInfo, lastName: e.target.value })}
                                        />
                                    </div>
                                </div>
                                <div>
                                    <label className="block text-sm font-medium text-gray-700 mb-1">
                                        Email Address
                                    </label>
                                    <input
                                        type="email"
                                        required
                                        className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                        value={passengerInfo.email}
                                        onChange={(e) => setPassengerInfo({ ...passengerInfo, email: e.target.value })}
                                    />
                                </div>
                                <div>
                                    <label className="block text-sm font-medium text-gray-700 mb-1">
                                        Phone Number
                                    </label>
                                    <input
                                        type="tel"
                                        required
                                        className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                        value={passengerInfo.phone}
                                        onChange={(e) => setPassengerInfo({ ...passengerInfo, phone: e.target.value })}
                                    />
                                </div>
                            </form>
                        </div>
                    </div>

                    {/* Booking Summary */}
                    <div className="lg:col-span-1">
                        <div className="bg-white rounded-lg shadow-md p-6 sticky top-6">
                            <h3 className="text-lg font-semibold text-gray-900 mb-4">Booking Summary</h3>

                            <div className="space-y-3 mb-6">
                                <div className="flex justify-between">
                                    <span className="text-gray-600">Flight</span>
                                    <span className="font-medium">{flight.flightNumber}</span>
                                </div>
                                <div className="flex justify-between">
                                    <span className="text-gray-600">Route</span>
                                    <span className="font-medium">{flight.origin} → {flight.destination}</span>
                                </div>
                                <div className="flex justify-between">
                                    <span className="text-gray-600">Date</span>
                                    <span className="font-medium">{new Date(flight.date).toLocaleDateString()}</span>
                                </div>
                                <div className="flex justify-between">
                                    <span className="text-gray-600">Time</span>
                                    <span className="font-medium">{flight.departureTime} - {flight.arrivalTime}</span>
                                </div>
                                {selectedSeat && (
                                    <div className="flex justify-between">
                                        <span className="text-gray-600">Seat</span>
                                        <span className="font-medium">{selectedSeat}</span>
                                    </div>
                                )}
                            </div>

                            <div className="border-t pt-4 mb-6">
                                <div className="flex items-center justify-between text-lg font-bold">
                                    <span>Total</span>
                                    <span>${flight.price}</span>
                                </div>
                            </div>

                            <button
                                onClick={handleBooking}
                                disabled={!selectedSeat || booking}
                                className="w-full bg-purple-600 hover:bg-purple-700 text-white py-3 rounded-md transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center space-x-2"
                            >
                                <CreditCard className="h-5 w-5" />
                                <span>{booking ? 'Processing...' : 'Proceed to Payment'}</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default BookFlight;