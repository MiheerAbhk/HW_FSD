import React, { useState, useEffect } from 'react';
import { Users, Plane, Calendar, TrendingUp } from 'lucide-react';
import api from '../../api/config';

const AdminDashboard = () => {
    const [stats, setStats] = useState({
        totalUsers: 0,
        totalFlights: 0,
        totalBookings: 0,
        revenue: 0
    });
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchStats();
    }, []);

    const fetchStats = async () => {
        try {
            // Mock data for demo
            setStats({
                totalUsers: 1250,
                totalFlights: 45,
                totalBookings: 890,
                revenue: 125000
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
                    <h1 className="text-3xl font-bold text-gray-900">Admin Dashboard</h1>
                    <p className="mt-2 text-gray-600">Welcome back! Here's what's happening with Simply Fly today.</p>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
                    <StatCard
                        title="Total Users"
                        value={stats.totalUsers}
                        icon={Users}
                        color="#3B82F6"
                    />
                    <StatCard
                        title="Total Flights"
                        value={stats.totalFlights}
                        icon={Plane}
                        color="#10B981"
                    />
                    <StatCard
                        title="Total Bookings"
                        value={stats.totalBookings}
                        icon={Calendar}
                        color="#F59E0B"
                    />
                    <StatCard
                        title="Total Revenue"
                        value={stats.revenue}
                        icon={TrendingUp}
                        color="#EF4444"
                        prefix="$"
                    />
                </div>

                <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                    <div className="bg-white rounded-lg shadow-md p-6">
                        <h3 className="text-lg font-semibold text-gray-900 mb-4">Recent Activity</h3>
                        <div className="space-y-4">
                            <div className="flex items-center space-x-3 p-3 bg-blue-50 rounded-lg">
                                <Users className="h-5 w-5 text-blue-600" />
                                <div>
                                    <p className="text-sm font-medium text-gray-900">New user registered</p>
                                    <p className="text-xs text-gray-500">2 hours ago</p>
                                </div>
                            </div>
                            <div className="flex items-center space-x-3 p-3 bg-green-50 rounded-lg">
                                <Plane className="h-5 w-5 text-green-600" />
                                <div>
                                    <p className="text-sm font-medium text-gray-900">New flight added</p>
                                    <p className="text-xs text-gray-500">4 hours ago</p>
                                </div>
                            </div>
                            <div className="flex items-center space-x-3 p-3 bg-yellow-50 rounded-lg">
                                <Calendar className="h-5 w-5 text-yellow-600" />
                                <div>
                                    <p className="text-sm font-medium text-gray-900">Flight booking confirmed</p>
                                    <p className="text-xs text-gray-500">6 hours ago</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="bg-white rounded-lg shadow-md p-6">
                        <h3 className="text-lg font-semibold text-gray-900 mb-4">System Status</h3>
                        <div className="space-y-4">
                            <div className="flex items-center justify-between p-3 bg-green-50 rounded-lg">
                                <span className="text-sm font-medium text-gray-900">API Status</span>
                                <span className="px-2 py-1 text-xs font-semibold text-green-800 bg-green-200 rounded-full">
                                    Online
                                </span>
                            </div>
                            <div className="flex items-center justify-between p-3 bg-green-50 rounded-lg">
                                <span className="text-sm font-medium text-gray-900">Database</span>
                                <span className="px-2 py-1 text-xs font-semibold text-green-800 bg-green-200 rounded-full">
                                    Connected
                                </span>
                            </div>
                            <div className="flex items-center justify-between p-3 bg-yellow-50 rounded-lg">
                                <span className="text-sm font-medium text-gray-900">Payment Gateway</span>
                                <span className="px-2 py-1 text-xs font-semibold text-yellow-800 bg-yellow-200 rounded-full">
                                    Testing
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AdminDashboard;