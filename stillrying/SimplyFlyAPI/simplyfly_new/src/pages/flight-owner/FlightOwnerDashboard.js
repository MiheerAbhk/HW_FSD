import React, { useState, useEffect } from 'react';
import { Plane, Calendar, Users, DollarSign } from 'lucide-react';
import api from '../../api/config';

const FlightOwnerDashboard = () => {
    const [stats, setStats] = useState({
        totalFlights: 0,
        totalBookings: 0,
        revenue: 0,
        activeRoutes: 0
    });
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchStats();
    }, []);

    const fetchStats = async () => {
        try {
            // Mock data for demo
            setStats({
                totalFlights: 12,
                totalBookings: 245,
                revenue: 45000,
                activeRoutes: 8
            });
        } catch (error) {
            console.error('Error fetching stats:', error);
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
                        {prefix}{loading ? '...' : value.toLocaleString()}
                    </p>
                </div>
                <Icon className="h-8 w-8" style={{ color }} />
            </div>
        </div>
    );

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-7xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">Flight Owner Dashboard</h1>
                    <p className="mt-2 text-gray-600">Manage your airline operations and track performance.</p>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
                    <StatCard
                        title="Total Flights"
                        value={stats.totalFlights}
                        icon={Plane}
                        color="#10B981"
                    />
                    <StatCard
                        title="Total Bookings"
                        value={stats.totalBookings}
                        icon={Users}
                        color="#3B82F6"
                    />
                    <StatCard
                        title="Revenue"
                        value={stats.revenue}
                        icon={DollarSign}
                        color="#F59E0B"
                        prefix="$"
                    />
                    <StatCard
                        title="Active Routes"
                        value={stats.activeRoutes}
                        icon={Calendar}
                        color="#8B5CF6"
                    />
                </div>

                <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                    <div className="bg-white rounded-lg shadow-md p-6">
                        <h3 className="text-lg font-semibold text-gray-900 mb-4">Recent Flights</h3>
                        <div className="space-y-4">
                            <div className="flex items-center justify-between p-3 bg-green-50 rounded-lg">
                                <div className="flex items-center space-x-3">
                                    <Plane className="h-5 w-5 text-green-600" />
                                    <div>
                                        <p className="text-sm font-medium text-gray-900">SF001 - NYC → LAX</p>
                                        <p className="text-xs text-gray-500">Departing at 2:30 PM</p>
                                    </div>
                                </div>
                                <span className="text-xs font-semibold text-green-800 bg-green-200 px-2 py-1 rounded-full">
                                    On Time
                                </span>
                            </div>
                            <div className="flex items-center justify-between p-3 bg-blue-50 rounded-lg">
                                <div className="flex items-center space-x-3">
                                    <Plane className="h-5 w-5 text-blue-600" />
                                    <div>
                                        <p className="text-sm font-medium text-gray-900">SF002 - CHI → MIA</p>
                                        <p className="text-xs text-gray-500">Departing at 5:45 PM</p>
                                    </div>
                                </div>
                                <span className="text-xs font-semibold text-blue-800 bg-blue-200 px-2 py-1 rounded-full">
                                    Boarding
                                </span>
                            </div>
                            <div className="flex items-center justify-between p-3 bg-yellow-50 rounded-lg">
                                <div className="flex items-center space-x-3">
                                    <Plane className="h-5 w-5 text-yellow-600" />
                                    <div>
                                        <p className="text-sm font-medium text-gray-900">SF003 - SEA → BOS</p>
                                        <p className="text-xs text-gray-500">Departing at 8:15 PM</p>
                                    </div>
                                </div>
                                <span className="text-xs font-semibold text-yellow-800 bg-yellow-200 px-2 py-1 rounded-full">
                                    Delayed
                                </span>
                            </div>
                        </div>
                    </div>

                    <div className="bg-white rounded-lg shadow-md p-6">
                        <h3 className="text-lg font-semibold text-gray-900 mb-4">Booking Analytics</h3>
                        <div className="space-y-4">
                            <div className="flex items-center justify-between">
                                <span className="text-sm text-gray-600">This Week</span>
                                <span className="text-sm font-semibold text-gray-900">45 bookings</span>
                            </div>
                            <div className="w-full bg-gray-200 rounded-full h-2">
                                <div className="bg-green-600 h-2 rounded-full" style={{ width: '75%' }}></div>
                            </div>

                            <div className="flex items-center justify-between">
                                <span className="text-sm text-gray-600">This Month</span>
                                <span className="text-sm font-semibold text-gray-900">245 bookings</span>
                            </div>
                            <div className="w-full bg-gray-200 rounded-full h-2">
                                <div className="bg-blue-600 h-2 rounded-full" style={{ width: '85%' }}></div>
                            </div>

                            <div className="flex items-center justify-between">
                                <span className="text-sm text-gray-600">Average Load Factor</span>
                                <span className="text-sm font-semibold text-gray-900">78%</span>
                            </div>
                            <div className="w-full bg-gray-200 rounded-full h-2">
                                <div className="bg-purple-600 h-2 rounded-full" style={{ width: '78%' }}></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default FlightOwnerDashboard;