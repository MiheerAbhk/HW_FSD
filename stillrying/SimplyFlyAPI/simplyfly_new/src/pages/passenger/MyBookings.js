import React, { useState, useEffect } from 'react';
import { Calendar, Plane, User, X } from 'lucide-react';
import api from '../../api/config';

const MyBookings = () => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [filter, setFilter] = useState('all');

    useEffect(() => {
        fetchBookings();
    }, []);

    const fetchBookings = async () => {
        try {
            // Mock bookings data
            setBookings([
                {
                    id: 1,
                    flightNumber: 'SF001',
                    airline: 'Simply Fly Airlines',
                    route: 'New York → Los Angeles',
                    date: '2024-01-25',
                    departureTime: '14:30',
                    arrivalTime: '17:45',
                    seatNumber: '12A',
                    status: 'Confirmed',
                    amount: 299.99,
                    bookingDate: '2024-01-20'
                },
                {
                    id: 2,
                    flightNumber: 'SF003',
                    airline: 'Simply Fly Airlines',
                    route: 'Seattle → Boston',
                    date: '2024-02-15',
                    departureTime: '20:15',
                    arrivalTime: '04:30',
                    seatNumber: '8B',
                    status: 'Confirmed',
                    amount: 349.99,
                    bookingDate: '2024-01-22'
                },
                {
                    id: 3,
                    flightNumber: 'SF002',
                    airline: 'Simply Fly Airlines',
                    route: 'Chicago → Miami',
                    date: '2023-12-20',
                    departureTime: '17:45',
                    arrivalTime: '20:30',
                    seatNumber: '15C',
                    status: 'Completed',
                    amount: 199.99,
                    bookingDate: '2023-12-15'
                }
            ]);
        } catch (error) {
            console.error('Error fetching bookings:', error);
        } finally {
            setLoading(false);
        }
    };

    const handleCancelBooking = async (bookingId) => {
        if (window.confirm('Are you sure you want to cancel this booking?')) {
            try {
                // Mock cancel operation
                setBookings(bookings.map(booking =>
                    booking.id === bookingId
                        ? { ...booking, status: 'Cancelled' }
                        : booking
                ));
            } catch (error) {
                console.error('Error cancelling booking:', error);
            }
        }
    };

    const filteredBookings = bookings.filter(booking => {
        if (filter === 'all') return true;
        if (filter === 'upcoming') return booking.status === 'Confirmed' && new Date(booking.date) > new Date();
        if (filter === 'completed') return booking.status === 'Completed';
        if (filter === 'cancelled') return booking.status === 'Cancelled';
        return true;
    });

    const getStatusColor = (status) => {
        switch (status) {
            case 'Confirmed':
                return 'bg-green-100 text-green-800';
            case 'Completed':
                return 'bg-blue-100 text-blue-800';
            case 'Cancelled':
                return 'bg-red-100 text-red-800';
            default:
                return 'bg-gray-100 text-gray-800';
        }
    };

    const canCancel = (booking) => {
        return booking.status === 'Confirmed' && new Date(booking.date) > new Date();
    };

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-6xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">My Bookings</h1>
                    <p className="mt-2 text-gray-600">View and manage your flight bookings</p>
                </div>

                {/* Filter Tabs */}
                <div className="bg-white rounded-lg shadow-md mb-6">
                    <div className="border-b border-gray-200">
                        <nav className="flex space-x-8 px-6">
                            {[
                                { key: 'all', label: 'All Bookings' },
                                { key: 'upcoming', label: 'Upcoming' },
                                { key: 'completed', label: 'Completed' },
                                { key: 'cancelled', label: 'Cancelled' }
                            ].map((tab) => (
                                <button
                                    key={tab.key}
                                    onClick={() => setFilter(tab.key)}
                                    className={`py-4 px-1 border-b-2 font-medium text-sm ${filter === tab.key
                                            ? 'border-purple-500 text-purple-600'
                                            : 'border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300'
                                        }`}
                                >
                                    {tab.label}
                                </button>
                            ))}
                        </nav>
                    </div>
                </div>

                {/* Bookings List */}
                {loading ? (
                    <div className="bg-white rounded-lg shadow-md p-8 text-center">
                        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-purple-600 mx-auto mb-4"></div>
                        <p className="text-gray-600">Loading your bookings...</p>
                    </div>
                ) : filteredBookings.length === 0 ? (
                    <div className="bg-white rounded-lg shadow-md p-8 text-center">
                        <Calendar className="h-16 w-16 text-gray-300 mx-auto mb-4" />
                        <p className="text-gray-600">No bookings found for the selected filter.</p>
                    </div>
                ) : (
                    <div className="space-y-4">
                        {filteredBookings.map((booking) => (
                            <div key={booking.id} className="bg-white rounded-lg shadow-md p-6">
                                <div className="flex flex-col lg:flex-row lg:items-center lg:justify-between">
                                    <div className="flex-1">
                                        <div className="flex items-center space-x-4 mb-4">
                                            <Plane className="h-6 w-6 text-purple-600" />
                                            <div>
                                                <h3 className="text-lg font-semibold text-gray-900">
                                                    {booking.flightNumber} - {booking.route}
                                                </h3>
                                                <p className="text-sm text-gray-500">{booking.airline}</p>
                                            </div>
                                            <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(booking.status)}`}>
                                                {booking.status}
                                            </span>
                                        </div>

                                        <div className="grid grid-cols-1 md:grid-cols-3 gap-4 text-sm">
                                            <div className="flex items-center space-x-2">
                                                <Calendar className="h-4 w-4 text-gray-400" />
                                                <div>
                                                    <p className="font-medium">Flight Date</p>
                                                    <p className="text-gray-600">{new Date(booking.date).toLocaleDateString()}</p>
                                                </div>
                                            </div>
                                            <div className="flex items-center space-x-2">
                                                <User className="h-4 w-4 text-gray-400" />
                                                <div>
                                                    <p className="font-medium">Seat</p>
                                                    <p className="text-gray-600">{booking.seatNumber}</p>
                                                </div>
                                            </div>
                                            <div>
                                                <p className="font-medium">Time</p>
                                                <p className="text-gray-600">{booking.departureTime} - {booking.arrivalTime}</p>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="lg:ml-8 mt-4 lg:mt-0 text-center lg:text-right">
                                        <p className="text-2xl font-bold text-gray-900 mb-2">
                                            ${booking.amount}
                                        </p>
                                        <div className="space-y-2">
                                            <p className="text-xs text-gray-500">
                                                Booked on {new Date(booking.bookingDate).toLocaleDateString()}
                                            </p>
                                            {canCancel(booking) && (
                                                <button
                                                    onClick={() => handleCancelBooking(booking.id)}
                                                    className="flex items-center space-x-1 text-red-600 hover:text-red-700 text-sm font-medium"
                                                >
                                                    <X className="h-4 w-4" />
                                                    <span>Cancel Booking</span>
                                                </button>
                                            )}
                                        </div>
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

export default MyBookings;