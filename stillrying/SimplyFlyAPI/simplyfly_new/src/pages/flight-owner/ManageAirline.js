import React, { useState, useEffect } from 'react';
import { Edit, Save, X, Plane, Mail, Phone, MapPin } from 'lucide-react';
import api from '../../api/config';

const ManageAirline = () => {
    const [airline, setAirline] = useState(null);
    const [isEditing, setIsEditing] = useState(false);
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [formData, setFormData] = useState({
        name: '',
        code: '',
        description: '',
        contactEmail: '',
        contactPhone: '',
        headquarters: ''
    });

    useEffect(() => {
        fetchAirline();
    }, []);

    const fetchAirline = async () => {
        try {
            // Mock data for demo
            const mockAirline = {
                id: 1,
                name: 'Simply Fly Airlines',
                code: 'SF',
                description: 'Your trusted partner for comfortable and affordable air travel.',
                contactEmail: 'info@simplyfly.com',
                contactPhone: '+1-800-FLY-SAFE',
                headquarters: 'New York, NY'
            };
            setAirline(mockAirline);
            setFormData(mockAirline);
        } catch (error) {
            console.error('Error fetching airline:', error);
        } finally {
            setLoading(false);
        }
    };

    const handleSave = async () => {
        setSaving(true);
        try {
            // Mock save operation
            await new Promise(resolve => setTimeout(resolve, 1000));
            setAirline(formData);
            setIsEditing(false);
        } catch (error) {
            console.error('Error saving airline:', error);
        } finally {
            setSaving(false);
        }
    };

    const handleCancel = () => {
        setFormData(airline);
        setIsEditing(false);
    };

    if (loading) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-green-600"></div>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-4xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">Manage Airline</h1>
                    <p className="mt-2 text-gray-600">Update your airline information and branding</p>
                </div>

                <div className="bg-white rounded-lg shadow-md p-6">
                    <div className="flex items-center justify-between mb-6">
                        <div className="flex items-center space-x-3">
                            <div className="bg-green-100 p-3 rounded-full">
                                <Plane className="h-8 w-8 text-green-600" />
                            </div>
                            <div>
                                <h2 className="text-xl font-semibold text-gray-900">
                                    {airline?.name || 'Your Airline'}
                                </h2>
                                <p className="text-sm text-gray-500">Code: {airline?.code}</p>
                            </div>
                        </div>
                        {!isEditing ? (
                            <button
                                onClick={() => setIsEditing(true)}
                                className="flex items-center space-x-2 bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded-md transition-colors"
                            >
                                <Edit className="h-4 w-4" />
                                <span>Edit</span>
                            </button>
                        ) : (
                            <div className="flex space-x-2">
                                <button
                                    onClick={handleSave}
                                    disabled={saving}
                                    className="flex items-center space-x-2 bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded-md transition-colors disabled:opacity-50"
                                >
                                    <Save className="h-4 w-4" />
                                    <span>{saving ? 'Saving...' : 'Save'}</span>
                                </button>
                                <button
                                    onClick={handleCancel}
                                    className="flex items-center space-x-2 bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-md transition-colors"
                                >
                                    <X className="h-4 w-4" />
                                    <span>Cancel</span>
                                </button>
                            </div>
                        )}
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                Airline Name
                            </label>
                            {isEditing ? (
                                <input
                                    type="text"
                                    value={formData.name}
                                    onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-green-500 focus:border-green-500"
                                />
                            ) : (
                                <p className="text-gray-900">{airline?.name}</p>
                            )}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                Airline Code
                            </label>
                            {isEditing ? (
                                <input
                                    type="text"
                                    value={formData.code}
                                    onChange={(e) => setFormData({ ...formData, code: e.target.value })}
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-green-500 focus:border-green-500"
                                    maxLength="3"
                                />
                            ) : (
                                <p className="text-gray-900">{airline?.code}</p>
                            )}
                        </div>

                        <div className="md:col-span-2">
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                Description
                            </label>
                            {isEditing ? (
                                <textarea
                                    value={formData.description}
                                    onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                                    rows="3"
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-green-500 focus:border-green-500"
                                />
                            ) : (
                                <p className="text-gray-900">{airline?.description}</p>
                            )}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                <div className="flex items-center space-x-2">
                                    <Mail className="h-4 w-4" />
                                    <span>Contact Email</span>
                                </div>
                            </label>
                            {isEditing ? (
                                <input
                                    type="email"
                                    value={formData.contactEmail}
                                    onChange={(e) => setFormData({ ...formData, contactEmail: e.target.value })}
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-green-500 focus:border-green-500"
                                />
                            ) : (
                                <p className="text-gray-900">{airline?.contactEmail}</p>
                            )}
                        </div>

                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                <div className="flex items-center space-x-2">
                                    <Phone className="h-4 w-4" />
                                    <span>Contact Phone</span>
                                </div>
                            </label>
                            {isEditing ? (
                                <input
                                    type="tel"
                                    value={formData.contactPhone}
                                    onChange={(e) => setFormData({ ...formData, contactPhone: e.target.value })}
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-green-500 focus:border-green-500"
                                />
                            ) : (
                                <p className="text-gray-900">{airline?.contactPhone}</p>
                            )}
                        </div>

                        <div className="md:col-span-2">
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                <div className="flex items-center space-x-2">
                                    <MapPin className="h-4 w-4" />
                                    <span>Headquarters</span>
                                </div>
                            </label>
                            {isEditing ? (
                                <input
                                    type="text"
                                    value={formData.headquarters}
                                    onChange={(e) => setFormData({ ...formData, headquarters: e.target.value })}
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-green-500 focus:border-green-500"
                                />
                            ) : (
                                <p className="text-gray-900">{airline?.headquarters}</p>
                            )}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ManageAirline;