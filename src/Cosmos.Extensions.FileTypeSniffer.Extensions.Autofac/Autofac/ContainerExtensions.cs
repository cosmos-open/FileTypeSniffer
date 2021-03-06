﻿using System;
using System.Collections.Generic;
using Cosmos.FileTypeSniffers;
using Cosmos.FileTypeSniffers.Core;
using Cosmos.FileTypeSniffers.Providers;
using Cosmos.FileTypeSniffers.Registering;

namespace Autofac
{
    public static class ContainerExtensions
    {
        public static ContainerBuilder RegisterFileTypeSniffer(this ContainerBuilder builder, Action<AutofacFileTypeSnifferOptions> optionAction = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var options = new AutofacFileTypeSnifferOptions();
            optionAction?.Invoke(options);

            var descriptors = new List<IFileTypeDescriptor>();

            //To Activate default descriptor provider with registrar scanner
            var defaultProvider = new DefaultFileTypeProvider();
            descriptors.AddRange(defaultProvider.GetDescriptors());

            //To Activate custom additional descriptor provider
            foreach (var provider in options.AdditionalDescriptorProvider)
            {
                descriptors.AddRange(provider.GetDescriptors());
            }

            var sniffer = FileTypeSnifferFactory.Create(descriptors, options.Separators?.ToArray() ?? null);
            builder.RegisterInstance(sniffer).As<IFileTypeSniffer>().SingleInstance();

            return builder;
        }
    }
}
