import React, { useState, useEffect } from 'react';
import { Calendar, Search, CreditCard, MapPin } from 'lucide-react';
import { Link } from 'react-router-dom';
import api from '../../api/config';

const PassengerDashboard = () => {
    const [stats, setStats] = useState({
        upcomingFlights: 0,
        totalBookings: 0,
        totalSpent: 0
    });
    const [recentBookings, setRecentBookings] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchDashboardData();
    }, []);

    const fetchDashboardData = async () => {
        try {
            // Mock data for demo
            setStats({
                upcomingFlights: 2,
                totalBookings: 8,
                totalSpent: 1250.00
            });

            setRecentBookings([
                {
                    id: 1,
                    flightNumber: 'SF001',
                    route: 'New York → Los Angeles',
                    date: '2024-01-25',
                    status: 'Confirmed',
                    seatNumber: '12A'
                },
                {
                    id: 2,
                    flightNumber: 'SF003',
                    route: 'Seattle → Boston',
                    date: '2024-02-15',
                    status: 'Confirmed',
                    seatNumber: '8B'
                }
            ]);
        } catch (error) {
            console.error('Error fetching dashboard data:', error);
        } finally {
            setLoading(false);
        }
    };

    const StatCard = ({ title, value, icon: Icon, color, prefix = '' }) => (
        <div className="bg-white rounded-lg shadow-md p-6 border-l-4" style={{ borderLeftColor: color }}>
            <div className="flex items-center justify-between">
                <div>
                    <p className="text-sm font-medium text-gray-600">{title}</p>
                    <p className="text-2xl font-bold text-gray-900">
                        {prefix}{loading ? '...' : value}
                    </p>
                </div>
                <Icon className="h-8 w-8" style={{ color }} />
            </div>
        </div>
    );

    const getStatusColor = (status) => {
        switch (status) {
            case 'Confirmed':
                return 'bg-green-100 text-green-800';
            case 'Pending':
                return 'bg-yellow-100 text-yellow-800';
            case 'Cancelled':
                return 'bg-red-100 text-red-800';
            default:
                return 'bg-gray-100 text-gray-800';
        }
    };

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-7xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">Welcome Back!</h1>
                    <p className="mt-2 text-gray-600">Plan your next journey with Simply Fly</p>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
                    <StatCard
                        title="Upcoming Flights"
                        value={stats.upcomingFlights}
                        icon={Calendar}
                        color="#8B5CF6"
                    />
                    <StatCard
                        title="Total Bookings"
                        value={stats.totalBookings}
                        icon={MapPin}
                        color="#3B82F6"
                    />
                    <StatCard
                        title="Total Spent"
                        value={stats.totalSpent.toFixed(2)}
                        icon={CreditCard}
                        color="#10B981"
                        prefix="$"
                    />
                </div>

                <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                    <div className="bg-white rounded-lg shadow-md p-6">
                        <h3 className="text-lg font-semibold text-gray-900 mb-4">Quick Actions</h3>
                        <div className="grid grid-cols-1 gap-4">
                            <Link
                                to="/passenger/search"
                                className="flex items-center space-x-3 p-4 bg-purple-50 hover:bg-purple-100 rounded-lg transition-colors"
                            >
                                <Search className="h-6 w-6 text-purple-600" />
                                <div>
                                    <p className="font-medium text-gray-900">Search Flights</p>
                                    <p className="text-sm text-gray-500">Find and book your next flight</p>
                                </div>
                            </Link>
                            <Link
                                to="/passenger/bookings"
                                className="flex items-center space-x-3 p-4 bg-blue-50 hover:bg-blue-100 rounded-lg transition-colors"
                            >
                                <Calendar className="h-6 w-6 text-blue-600" />
                                <div>
                                    <p className="font-medium text-gray-900">My Bookings</p>
                                    <p className="text-sm text-gray-500">View and manage your bookings</p>
                                </div>
                            </Link>
                        </div>
                    </div>

                    <div className="bg-white rounded-lg shadow-md p-6">
                        <h3 className="text-lg font-semibold text-gray-900 mb-4">Recent Bookings</h3>
                        <div className="space-y-4">
                            {loading ? (
                                <div className="animate-pulse space-y-3">
                                    <div className="h-4 bg-gray-200 rounded"></div>
                                    <div className="h-4 bg-gray-200 rounded"></div>
                                    <div className="h-4 bg-gray-200 rounded"></div>
                                </div>
                            ) : recentBookings.length > 0 ? (
                                recentBookings.map((booking) => (
                                    <div key={booking.id} className="flex items-center justify-between p-3 bg-gray-50 rounded-lg">
                                        <div className="flex items-center space-x-3">
                                            <Calendar className="h-5 w-5 text-purple-600" />
                                            <div>
                                                <p className="text-sm font-medium text-gray-900">
                                                    {booking.flightNumber} - {booking.route}
                                                </p>
                                                <p className="text-xs text-gray-500">
                                                    {new Date(booking.date).toLocaleDateString()} • Seat {booking.seatNumber}
                                                </p>
                                            </div>
                                        </div>
                                        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(booking.status)}`}>
                                            {booking.status}
                                        </span>
                                    </div>
                                ))
                            ) : (
                                <div className="text-center py-8">
                                    <Calendar className="h-12 w-12 text-gray-300 mx-auto mb-4" />
                                    <p className="text-gray-500">No recent bookings</p>
                                    <Link
                                        to="/passenger/search"
                                        className="text-purple-600 hover:text-purple-700 text-sm font-medium"
                                    >
                                        Book your first flight
                                    </Link>
                                </div>
                            )}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default PassengerDashboard;