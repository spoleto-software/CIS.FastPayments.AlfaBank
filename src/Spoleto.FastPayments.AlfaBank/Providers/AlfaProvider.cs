﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Spoleto.FastPayments.AlfaBank.Helpers;
using Spoleto.FastPayments.AlfaBank.Models;
using Microsoft.Extensions.Logging;

namespace Spoleto.FastPayments.AlfaBank.Providers
{
    /// <summary>
    /// Провайдер для работы с хостом Альфа-Банка для оплаты покупок через СБП.
    /// </summary>
    public partial class AlfaProvider : IAlfaProvider
    {
        private readonly ILogger<AlfaProvider> _logger;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        public AlfaProvider(ILogger<AlfaProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Запрос QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="certificate">Сертификат для подписи запроса.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        public async Task<QRCodeResponseModel> GetQRCodeAsync(AlfaOption settings, Certificate certificate, QRCodeRequestModel requestModel, bool throwIfErrorCodeIsFailed)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeResponseModel>(settings, certificate, uri, HttpMethod.Post, jsonModel, throwIfErrorCodeIsFailed).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Запрос статуса оплаты QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="certificate">Сертификат для подписи запроса.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        public async Task<QRCodeStatusResponseModel> GetQRCodeStatusAsync(AlfaOption settings, Certificate certificate, QRCodeStatusRequestModel requestModel, bool throwIfErrorCodeIsFailed)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeStatusResponseModel>(settings, certificate, uri, HttpMethod.Post, jsonModel, throwIfErrorCodeIsFailed).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Запрос возможности проведения возврата по успешной оплате QR-кода.
        /// </summary>
        /// <remarks>
        /// Возврат состоит из 2-х последовательных запросов:
        /// <list type="number">
        ///     <item>
        ///         <term>GetQRCreversalData</term>
        ///         <description>это предварительная проверка возврата, а далее методом QRCreversal выполнить возврат операции.</description>
        ///     </item>
        ///     <item>
        ///         <term>QRCreversal</term>
        ///         <description>по возврату не делается запрос статуса, т.к отсутствие ошибок ("ErrorCode": 0) в методе QRCreversal означает успешно проведённую операцию возврата.</description>
        ///     </item>     
        /// </list>
        /// </remarks>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="certificate">Сертификат для подписи запроса.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        public async Task<QRCodeReversalDataResponseModel> GetQRCodeReversalDataAsync(AlfaOption settings, Certificate certificate, QRCodeReversalDataRequestModel requestModel, bool throwIfErrorCodeIsFailed)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeReversalDataResponseModel>(settings, certificate, uri, HttpMethod.Post, jsonModel, throwIfErrorCodeIsFailed).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Запрос возврата оплаты QR-кода.
        /// </summary>
        /// <remarks>
        /// Возврат состоит из 2-х последовательных запросов:
        /// <list type="number">
        ///     <item>
        ///         <term>GetQRCreversalData</term>
        ///         <description>это предварительная проверка возврата, а далее методом QRCreversal выполнить возврат операции.</description>
        ///     </item>
        ///     <item>
        ///         <term>QRCreversal</term>
        ///         <description>по возврату не делается запрос статуса, т.к отсутствие ошибок ("ErrorCode": 0) в методе QRCreversal означает успешно проведённую операцию возврата.</description>
        ///     </item>     
        /// </list>
        /// </remarks>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="certificate">Сертификат для подписи запроса.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        public async Task<QRCodeReversalResponseModel> GetQRCodeReversalAsync(AlfaOption settings, Certificate certificate, QRCodeReversalRequestModel requestModel, bool throwIfErrorCodeIsFailed)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeReversalResponseModel>(settings, certificate, uri, HttpMethod.Post, jsonModel, throwIfErrorCodeIsFailed).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        ///  Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="certificate">Сертификат для подписи запроса.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        public async Task<QRСodeCashLinkResponseModel> RegQRСodeCashLinkAsync(AlfaOption settings, Certificate certificate, QRСodeCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRСodeCashLinkResponseModel>(settings, certificate, uri, HttpMethod.Post, jsonModel, throwIfErrorCodeIsFailed).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="certificate">Сертификат для подписи запроса.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        public async Task<QRСodeActivateCashLinkResponseModel> ActivateQRСodeCashLinkAsync(AlfaOption settings, Certificate certificate, QRСodeActivateCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRСodeActivateCashLinkResponseModel>(settings, certificate, uri, HttpMethod.Post, jsonModel, throwIfErrorCodeIsFailed).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="certificate">Сертификат для подписи запроса.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        public async Task<QRСodeDeactivateCashLinkResponseModel> DeactivateQRСodeCashLinkAsync(AlfaOption settings, Certificate certificate, QRСodeDeactivateCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRСodeDeactivateCashLinkResponseModel>(settings, certificate, uri, HttpMethod.Post, jsonModel, throwIfErrorCodeIsFailed).ConfigureAwait(false);

            return result;
        }
    }
}
